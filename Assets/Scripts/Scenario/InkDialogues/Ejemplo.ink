VAR correctAnswers = 0


-> main

=== main ===
Esto es una historia de ejemplo
Aqui es donde se pueden escribir los dialogos de los personajes
Esta es la parte donde puedes elegir que hacer con tu vida
    * [Quiero ser una buena persona]
        Que bueno oir eso
    * [Quiero ser una mala persona]
        Taien si
- Ahora que hiciste esa elección, podemos seguir con el ejemplo
Podemos elegir hacer otra eleccion que nos puede llevar a elecciones anidadas
    * [¿En serio?]
        Sí, mira ahora tienes otra eleccion
        * * [Poggers]
                Ahora, sigamos a la seccion final
                -> seccion_final
    * [No quiero verlo]
        Ok, entonces vamos al final
        ->seccion_final
        
= seccion_final
En esta sección, probaremos modificar una variable a partir de las elecciones de diálogo.
Responde estas preguntas para cambiar el valor de la variable.
¿De qué color es el pasto?
    * [Verde]
        Bien
        ~ correctAnswers = correctAnswers + 1
    * [Azul]
        Nope
- Siguiente pregunta
¿Cuánto es 2+2?
    * [4]
        Pog
        ~ correctAnswers = correctAnswers + 1
    * [5]
        Interesante...
- Siguiente pregunta
Escoge la primera alternativa
    * [Esta sí]
        ~ correctAnswers = correctAnswers + 1
    * [Esta no]
- Hemos terminado, en alguna parte deberías poder ver tu puntaje final
-> END

->END