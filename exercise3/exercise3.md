# Exercise 3 

## Exercise 1: Reducing expressions

Given the definition double x = x + x, calculate the evaluations of double (double 2) using 
(a) call-by-name evaluation and 
(b) call-by-value evaluation. 

Hint: similar evaluations can be found in the slide nr 43.

### Call by value

1. double (double 2)
2. = { applying double }
3. double (2 + 2)
4. = {applying +}
5. double 4
6. = { applying double }
7. 4 + 4
8. = { applying + }
9. 8

### Call by name

1. double (double 2)
2. = { applying double }
3. (double 2) + (double 2)
4. = { applying first double }
5. (2 + 2) + (double 2)
6. = { applying second double }
7. = (2 + 2) + (2 + 2)
8. = { applying first + }
9. = 4 + (2 + 2)
10. = { applying second + }
11. = 4 + 4
12. = { applying + }
131. = 8

___

## Exercise 2: Practical implications

Think about practical implementations of the lecture. 

**What is an advantage of call-by-name evaluation over call-by-value evaluation?**

Es kann effizienter sein, da Argumente einer Funktion nicht ausgewertet werden, wenn diese nicht benötigt werden. Zum Beispiel kann ein Argument theoretisch sehr lange zum Auswerten brauchen, aber es wird in der Funktion gar nicht benötigt, dann wird es auch nicht ausgewertet. Es ist auch sehr gut für Operationen auf Listen, da die Berechnungen nur auf den Elementen gemacht wird, wo diese auch gebraucht wird. Wenn man zum Beispiel Berechnungen auf allen Elementen der Liste macht und dann am Ende nur das erste Element braucht, dann werden diese Berechnungen im Endeffekt nur auf dem ersten Element ausgeführt.


**What is the use of lazy evaluation?**

Lazy Evaluation hat im Vergleich zu Call by Name noch den Vorteil, dass Zwischenergebnisse gecached werden. Zum Beispiel beim Berechnen der rekursiven Fibonacci Funktion. Hier wird immer wieder das gleiche Zwischenergebnis berechnet. Durch das Caching ist die Auswertung deutlich schneller.