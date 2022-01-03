module lecture_files.lecture3.tdd

//define a "safe" email address type

type EmailAddress = private { verifiedAddress : string }

// Create an instance of an email-address.
// Type system recognizes that this expression is of type EmailAddress
let testEmail = {verifiedAddress = "test-email@gmail.com"}

type EmailValidation = 
    | Ok of EmailAddress
    | ValidationError of string

module Email =

    //define a function that uses it 
    let sendEmail (email : EmailAddress) = 
       printfn "sent an email to %s" email.verifiedAddress

    // function creates an email from a string.
    let ofString (s : string) : EmailValidation =
        // TODO: proper validation of string
        Ok { verifiedAddress = s }

//try to send one
let aliceEmail = Email.ofString "alice@example.com"

// Check if the email was validated successfully.
match aliceEmail with
| Ok emailAddress ->  // email was ok
    Email.sendEmail emailAddress
| _ ->  // otherwise
    failwith "handle e.g. by user interface"

//try to send a plain string
//Email.sendEmail "bob@example.com"   //error

// Domain.fs

type Product =
    { name : string
      price : decimal }

type Vendor =
    | Visa
    | MasterCard

type PaymentMethod =
    | Stripe
    | PayPal
    | CreditCard of Vendor

type BounceReason =
    | PaymentProcessorFailed
    | CustomerWentOutOfMoney
    | CustomerCanceled

type PaymentResult =
    | Success
    | Bounce of BounceReason


type OrderQuantity = { count : int }
// guard constructions against inproper values (same as for email address)

type ShopCartLine =
    { quantity : OrderQuantity; item : Product }

type UnpaidCart = { items : list<ShopCartLine> }

type ShoppingCart =
    | PaidCart of list<ShopCartLine>
    | UnpaidCart of UnpaidCart

type CheckOut = UnpaidCart -> PaymentMethod -> PaymentResult
type AddToCart = UnpaidCart -> Product -> OrderQuantity -> UnpaidCart

type ShoppingCartApi = 
    {
        checkout : CheckOut
        add      : AddToCart
        empty    : UnpaidCart
    }

// CartImplemenation.fs
module Implementation = 
    
    let private addToCart { items = chartLine } product quantity = 
        let newItem = { quantity = quantity; item = product }
        { items = newItem :: chartLine }

    let private checkout { items = chartLine } (paymentMethod : PaymentMethod) =
        Bounce BounceReason.PaymentProcessorFailed

    let private emptyCart = { items = [] }

    let cartApi : ShoppingCartApi = 
        {
            add = addToCart
            checkout = checkout
            empty = emptyCart
        }

// in user code (UI/Domainlogic part)
open Implementation

let cart = cartApi.empty 
let cart' = cartApi.add cart { name = "fp"; price = 10.0M } { count = 1 }
let result = cartApi.checkout cart' PaymentMethod.Stripe