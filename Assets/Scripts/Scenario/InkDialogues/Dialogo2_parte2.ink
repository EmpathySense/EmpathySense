VAR correctAnswers = 0

VAR scoreSectionA = 0
VAR scoreSectionB = 0
VAR scoreSectionC = 0
VAR scoreSectionD = 0
VAR scoreSectionE = 0

VAR mistakes = 0

->five_choice

 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1
 	
=== function mistake_dialogue()
    (Espera, eso no tiene sentido)


/*
=== second_choice ===
¿Qué debería responder? #title:Tú
    * Hola, JORGE, percibo que estás pasando un mal momento, si me permites, me gustaría acompañarte
    ~ raise(correctAnswers)
    No sé qué me pasa, estoy colapsado. #title:Jorge 
    ->third_choice
    * Hola, JORGE ¿Qué pasó? ¿Por qué estás llorando?
    ~ raise(mistakes)
    ->third_choice
    * Hola, JORGE, la NOM-COMPAÑERx me dijo que no parabas de llorar
    ~ raise(mistakes)
    ->second_choice
 
 
//OPCIÓN SALIR DE LA SALA EN QUE SE ENCUENTRAN
 
 
 === third_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo te puedo ayudar, sé qué hacer cuando alguien tiene una crisis.
    ~ raise(mistakes)
    * Cuéntame qué pasó, seguro lo podemos solucionar.
    ~ raise(mistakes)
    * Te entiendo, a veces nos vemos abrumados 
    ~ raise(correctAnswers)
    
-\(Asiente, mantiene llanto) #title:Jorge
->four_choice


 === four_choice === 
 ¿Qué debería responder? #title:Tú
    * ¿Te sientes cómodo acá? Podemos ir a otro lado si lo prefieres
    ~ raise(correctAnswers)
    * Vamos a otro lado, para no molestar a nadie acá
    ~ raise(mistakes)
    * ¿Te parece si salimos? Todos te están mirando. 
    ~ raise(mistakes)
    
- Mejor salgamos \(Mantiene llanto) no sé qué podría decir alguien acá. #title:Jorge
->five_choice
*/
 
/* PASO A  */

 
 === five_choice === 
 ¿Qué debería responder? #title:Tú
    * Jorge, veo que no estás pasándolo muy bien[] ahora, entonces si te sientes cómodo y quieres comentarte algo o desahogarte yo tengo la disposición de escucharte.
        ~ raise(scoreSectionA)
    * ¿Qué te pasó?
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->five_choice
    * ¿Por qué estás llorando? 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->five_choice
    
- Bueno, estos últimos días he tenido muchas cosas en mente. #title:Jorge
- Recuerda que la persona que está en una crisis de pánico puede estar cerrada al diálogo, debes respetar lo que decida. #title:Consejo
->six_choice


 === six_choice === 
 ¿Qué debería responder? #title:Tú
    * ¿Por qué crees que no eres valorado?
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->six_choice
    * Entiendo, debe ser difícil para ti sentir eso.
        ~ raise(scoreSectionA)
    * Ah mí me pasa igual, de hecho, el otro día fui hablar de esto con el jefe.
        ~ raise(mistakes)
        -No me interesa tu conversación con el jefe.#title:Jorge
        ->six_choice
    
- Si, porque tengo más trabajo del que puedo hacer, aparte cada fecha límite se pisa con la otra, no termino con una cuando ya tengo que empezar con otra tarea. #title:Jorge 
->seven_choice


 === seven_choice ===
 ¿Qué debería responder? #title:Tú
    * Comprendo lo que me cuentas, te refieres a sentirte con sobrecarga laboral
        ~ raise(scoreSectionA)
    * Todos estamos igual de sobrepasados
        - Así te veo. #title:Jorge
        ~ raise(mistakes)
        ->seven_choice
    * Yo tengo un método que me funciona muy bien para organizar las tareas
        - No creo que me sirva ahora tu método.#title:Jorge
        ~ raise(mistakes)
        ->seven_choice
    
- Sí, de hecho llego a la casa y apenas duermo intentando terminar lo que me falta del día, me siento cansado y estoy lleno de ojeras. #title:Jorge
- Hasta sueño con el trabajo, luego por la mañana despierto para llenarme de café y seguir trabajando. #title:Jorge
->eight_choice

 === eight_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Dijiste que soñabas incluso con el trabajo?
        ~ raise(scoreSectionA)
    * A veces nos tocan momentos difíciles 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->eight_choice
    * No hay mal que por bien no venga[.], quizá tienes que pasar esto para que pasen otras cosas mejores 
        - No es nada agradable que te hagan pasar por esto.#title:Jorge
        ~ raise(mistakes)
        ->eight_choice
    
- Sí, y eso es lo peor porque ni descansando me puedo librarme de él, lo que quiero es descansar, tomar vacaciones e irme un tiempo de acá, pero no puedo, porque no me dan vacaciones y sobretodo porque no tengo plata para hacerlo. #title:Jorge

- \(Habla un poco más rápido y está levemente agitado) #title:Jorge #animation:TrSittingDisbelief1

- Además me siento excluido acá, en el horario de colación o a la salida todos están con sus grupos y yo me quedo apartado. Creo que no les caigo bien. #title:Jorge
->nine_choice


 === nine_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Dices que te sientes excluido?
        ~ raise(scoreSectionA)
    * A mí también me ha pasado que a veces he sentido esa soledad 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->nine_choice
    * Pero no te sientas mal[.], si ellos son los que se lo pierden, a mí me caes bien 
        - Que alentador tu comentario muchas gracias \(tono sarcastico). #title:Jorge
        ~ raise(mistakes)
        ->nine_choice

-Sí, bueno quizá sea que no he tenido muchas oportunidades para que me conozcan. Después del trabajo no me quedan muchas energías para nada, me voy a la casa apenas salgo. ¡Estoy chato! #title:Jorge #animation:TrSittingDisbelief2
- \(Es notorio que se encuentra agitado) #title:Jorge
->ten_choice

/* PASO B  */


 === ten_choice ===
 ¿Qué debería responder? #title:Tú
    * Si me dices las cosas que te preocupan, podemos ordenarlas e ir mejorando la situación.
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->ten_choice
    * [Conozco un ejercicio de respiración] Mira, percibo que estás algo angustiado, yo conozco un ejercicio de respiración que me ha servido harto en este tipo de situaciones para encontrar calma, ¿te gustaría que lo probemos?  
        ~ raise(scoreSectionB)
    * Intenta respirar mejor para calmarte, fíjate como lo hago 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->ten_choice

-No creo que ayude. #title:Jorge
->eleven_choice


 === eleven_choice ===
 ¿Qué debería responder? #title:Tú
    * Tienes que hacer lo que yo hago, mírame bien 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->eleven_choice
    * ¿Qué te parece si lo intentamos? Yo te acompaño, y vemos si nos sirve.   
        ~ raise(scoreSectionB)
    * \(Comenzar a respirar esperando que te siga)  
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->eleven_choice

-\(Asiente) Está bien. #title:Jorge
->twelve_choice


 === twelve_choice ===
 ¿Qué debería responder? #title:Tú
    * Quizás has escuchado lo de respirar consciente y profundo.[] En realidad, sirve bastante, mira te voy a explicar cómo podemos hacerlo  
        ~ raise(scoreSectionB)
    * Tienes que hacer la respiración de 4 tiempos, con eso estás listo   
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->twelve_choice

-\(Asiente) #title:Jorge #animation:TrSittingLookDown_1
->thirteen_choice


=== thirteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Haremos la técnica de respiración de 4 tiempos[.], primero tomas aire en 4 tiempos, luego botas el aire en 4 tiempos y finalmente hay que mantener los pulmones "vacíos" por 4 tiempos   
        ~ raise(scoreSectionB)
    * Es fácil, es inhalar, exhalar y mantener sin aire   
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->thirteen_choice
    * Mira obsérvame primero, en lo que hago y entenderás mejor  
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->thirteen_choice

-\(Asiente) #title:Jorge
->fourteen_choice

=== fourteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Intentémoslo, hazlo contando a un ritmo que te sea cómodo.[] Inhala 1, 2, 3, 4, exhala 1, 2, 3, 4, ahora mantén 1, 2, 3, 4
        ~ raise(scoreSectionB)
    * Veamos si te sale a la primera o no
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->fourteen_choice
    * Tienes que hacerlo bien, o si no te vas a agitar más
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->fourteen_choice 
    

- Recuerda que los 4 tiempos no son necesariamente 4 segundos, puede variar según el estado de agitación de la persona #title:Consejo
- \(Mejora poco a poco la respiración)  #title:Jorge
->fiveteen_choice

=== fiveteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Lo estás haciendo muy bien, sigamos repitiendo el ejercicio  
        ~ raise(scoreSectionB)
    * \¡Bien Jorge! No esperaba que te saliera a la primera, pero lo hiciste súper bien.
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->fiveteen_choice 
    * Qué bueno que ahora estás mejor, ¿qué te había pasado?  
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->fiveteen_choice 

-\(Asiente) #title:Jorge #animation:TrSittingMovingLegs
- Vamos de nuevo, inhala 1, 2, 3, 4, exhala 1, 2, 3, 4, ahora mantén 1, 2, 3, 4 #title:Tú
- Recuerda que este paso dura aproximadamente 10 minutos, pudiendo incluso tardar más, debes asegurarte de que la persona recuperó la calma. #title:Consejo
- \(respira normal, notablemente más calmado)
->sixteen_choice

=== sixteen_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Qué tal? ¿Te sientes un poco más calmado?  
        ~ raise(scoreSectionB)
    * Que bien que estás más calmado, pensé que íbamos a estar más tiempo con eso
        -Perdon por usar tu tiempo.
        ~ raise(mistakes)
        ->sixteen_choice 
    * Viste si era fácil, hasta tú pudiste hacerlo 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->sixteen_choice 

-Si, gracias, ahora me siento un poco mejor. #title:Jorge

->seventeen_choice

=== seventeen_choice ===
 ¿Qué debería responder? #title:Tú
    * Este es un muy buen ejercicio [, puedes aplicarlo en varias situaciones], puedes aplicarlo en esta situación o alguna parecida donde creas perder la calma. También vi que recomiendan practicarlo al despertar o antes de dormir, para mejorar sus resultados.  
        ~ raise(correctAnswers)
    * Menos mal estaba cerca, quizá que hubiese pasado, ¡pudiste desmayarte!
        -No digas eso que me preocupo.
        ~ raise(mistakes)
        ->seventeen_choice
    * Es bueno este ejercicio, para la otra ya sabes que hacer, ¡no se te vaya a olvidar! 
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->seventeen_choice 

-Gracias, lo tendré en cuenta. Estos días me haría muy bien al dormir sobretodo. #title:Jorge #animation:TrSittingTalking

->eighteen_choice

/* PASO C  */


=== eighteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Vamos de vuelta a la oficina y vemos si puedes volver a trabajar
        -De momento dejame solo si estas apurado.
        ~ raise(mistakes)
        ->eighteen_choice
    * Yo creo que podrías hablar con un terapeuta o alguien profesional
        -No tengo tiempo para esto.
        ~ raise(mistakes)
        ->eighteen_choice
    * [¿Qué sientes que es lo más necesitas resolver ahora?] Jorge, ahora que te sientes más tranquilo y retomando lo que me contaste, ¿Qué sientes que es lo más necesitas resolver ahora? 
        ~ raise(scoreSectionC)

-El tema del trabajo, es que últimamente es lo único que hago, tengo tanto que hacer y todo acumulado, que ya ni sé por dónde empezar, solo me estreso de pensarlo. #title:Jorge

->nineteen_choice

=== nineteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo creo que tienes que tomar licencia un par de días o semanas y vuelves renovado. 
        -No le veo beneficioso para todo lo que tengo que hacer.
        ~ raise(mistakes)
        ->nineteen_choice
    * Es que te falta organización, quizá dejas mucho tiempo muerto
        -Perdon sabio de la organización.
        ~ raise(mistakes)
        ->nineteen_choice
    * Entonces en lo que dices lo más importante de abordar ahora para ti es el trabajo, en relación al tiempo y la sobrecarga 
        ~ raise(scoreSectionC)

-Si es que antes yo sólo trabajaba media jornada, y tenía un poco más tiempo libre, ahí iba al cine o salía con excompañeros de la universidad, creo que eso me alivianaba un poco la carga que tenía. #title:Jorge

->twenty_choice

=== twenty_choice ===
 ¿Qué debería responder? #title:Tú
    * Comprendo, y como decías ahora tienes poco tiempo y te es difícil hacer esas actividades. 
        ~ raise(scoreSectionC)
    * Pero tómate un día de estos para salir o hacer algo, no creo que pase nada acá, el jefe ni se fija cuando falta alguien.
        -Claro, suena muy fácil hacerlo no cuando tienes tantas cosas que hacer.
        ~ raise(mistakes)
        ->twenty_choice
    * Ahora estamos mayores, nos toca dejar algunas cosas de lado.
        -No es tan fácil.
        ~ raise(mistakes)
        ->twenty_choice

-Claro, además de que tengo poco tiempo, tengo poca plata, después de pagar el arriendo tengo que estirar lo que me queda para pasar el mes, todo está súper caro. #title:Jorge

->twenty_one_choice

=== twenty_one_choice ===
 ¿Qué debería responder? #title:Tú
    * [Ordenando lo que me comentaste...] Entonces, ordenando lo que me comentaste, me decías que tienes mucho trabajo, poco tiempo, y estás muy justo con el dinero que ganas ¿Y de esto, que crees que es lo que más te aliviaría resolver primero? 
        ~ raise(scoreSectionC)
    * Si necesitas plata yo te puedo prestar, ya que tengo casa no pago arriendo.
        -No es un tema de plata.
        ~ raise(mistakes)
        ->twenty_one_choice
    * Podrías cambiarte a algún lugar que te salga más barato, como vives solo no creo que necesites mucho espacio.
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->twenty_one_choice

-Yo creo que la carga laboral, con eso organizado mejor creo poder manejar el resto. #title:Jorge

->twenty_two_choice

=== twenty_two_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo te puedo ayudar con eso, voy a hablar con el jefe y que te dé más plazo para entregar lo que te falta. 
        -Ya sabes como es no le importa nuestra estado.
        ~ raise(mistakes)
        ->twenty_two_choice
    * Yo hago lo que te falte, soy rápido con eso, así quedas más libre
        ~ raise(mistakes)
        ~ mistake_dialogue()
        ->twenty_two_choice
    * ¿Hay alguna forma en la que sientes que podrías atender eso o recibir ayuda al respecto?
        ~ raise(scoreSectionC)

-No lo sé, no he pensado en muchas soluciones #title:Jorge

->twenty_three_choice


/* PASO D  */


=== twenty_three_choice ===
 ¿Qué debería responder? #title:Tú
    * Sé que a veces no es fácil lidiar con esto por tu cuenta. ¿Has considerado la posibilidad de hablar con un terapeuta o psicólogo?  
        ~ raise(scoreSectionD)
    * Voy a ir a recursos humanos para que pueda hablar con el resto del trabajo y que sepan tu situación
        -Esos buenos para nada nunca nos han ayudado.
        ~ raise(mistakes)
        ->twenty_three_choice
    * Mira pásame tu celular, podemos llamar a algún familiar para que sepa lo que te pasó y ver si te puede ayudar.
        -No quiero molestar a nadie.
        ~ raise(mistakes)
        ->twenty_three_choice

-No sé, es primera vez que me pasa esto, no creo que sea necesario. #title:Jorge

->twenty_four_choice

=== twenty_four_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo creo que tienes que ir al psicólogo, podría ser la primera de muchas
        -No confio mucho.
        ~ raise(mistakes)
        ->twenty_four_choice
    * Entiendo que quizás desconfíes de ese tipo de apoyo[.], pero si más adelante cambias de opinión, si quieres te puedo entregar algunos datos.
        ~ raise(scoreSectionD)
    * Es importante que vayas sí o sí a ver un profesional.
        -¿Tan mal crees que estoy?
        ~ raise(mistakes)
        ->twenty_four_choice

-Bueno, quien sabe. #title:Jorge

->twenty_five_choice

=== twenty_five_choice ===
 ¿Qué debería responder? #title:Tú
    * Acá en el trabajo puedes hablar conmigo cuando necesites.
        -No te preocupes, no te quiero quitar de tu tiempo.
        ~ raise(mistakes)
    * Sé que la empresa tiene varios convenios[.] con diversos lugares donde puedes ir, entras a intranet y pones convenios, ahí puedes ver la información con más detalle y el contacto de cada uno.
        ~ raise(scoreSectionD)
    * Ahora no me acuerdo bien, pero buscas en Google y te aparecen varios lugares.
        -Y como sabre cual de todos esos es la mejor decisión para mi situación.
        ~ raise(mistakes)
        ->twenty_five_choice

-Lo revisaré, porque tampoco tengo plata para alguna consulta muy cara. #title:Jorge

-Con el convenio quedan súper accesible, igual te puedo recomendar otros que son baratos. Y si tienes alguna duda puedes llamar a Salud Responde (600 360 7777) que está disponible las 24 horas y pueden darte recomendaciones. #title:Tú
-Gracias, lo tendré en consideración, voy a guardar el número para no olvidarlo. #title:Jorge

->talk_about_psychoeducation

=== talk_about_psychoeducation ===
Jorge, ahora que te veo más en calma, te quería agradecer tu confianza porque sé que pasaste un momento complejo hoy. #title:Tú
A ti, gracias por acompañarme. Me siento más tranquilo de hecho. #title:Jorge

Ahora que estás más tranquilo, igual es bueno que sepas que estos procesos de crisis que a veces pasamos mantienen los síntomas algunos días. Es posible que sientas tristeza, rabia, tengas pesadillas, u otros, pero debes saber que son respuestas normales. #title:Tú
Comprendo, es que nunca me había pasado algo así. #title:Jorge

->twenty_six_choice


/* PASO E  */


=== twenty_six_choice ===
 ¿Qué debería responder? #title:Tú
    * [Si te sientes así de nuevo, recuerda lo que te ayudó a recuperar la calma ahora] Si sientes que vuelves a experimentar algo similar o una situación parecida a lo que experimentaste hoy, puedes recordar lo que te ayudó a recuperar la calma e implementarlo si lo necesitas.  
        ~ raise(scoreSectionE)
    * Si quieres guarda mi número y podemos conversar, generalmente respondo rápido y estoy pendiente.
        -Bueno, pero no quiero ser molestia constante.
        ~ raise(mistakes)
        ->twenty_six_choice
    * Lo hiciste súper hoy, ya está listo para una próxima vez.
        -¿Tan pronto me puede pasar de nuevo?
        ~ raise(mistakes)
        ->twenty_six_choice
        
-\(Asiente) Bueno, ¿Cómo el ejercicio de respiración? #title:Jorge

->twenty_seven_choice

=== twenty_seven_choice ===
 ¿Qué debería responder? #title:Tú
    * [Sí, ademas hay otras estrategias como...] Exacto, puedes aplicarlo cuando te sientas un poco agitado o estresado, también hay otras estrategias que se ha visto que sirven harto, como mantener horarios de descansos regulares y compartir con amigos o familiares  
        ~ raise(scoreSectionE)
    * Claro, si estás estresado recuerda lo que te enseñé y eso te va a servir para todo
        -Pero en el momento podría estar pensando muchas cosas y no saber que hacer.
        ~ raise(mistakes)
        ->twenty_seven_choice
    * Hay muchas más cosas que te pueden ayudar, pero ya tienes que ver tú que te sirve
        ~ mistake_dialogue()
        ~ raise(mistakes)
        ->twenty_seven_choice

-Me siento mejor sabiendo que es más normal de lo que pensaba.  #title:Jorge

->final_talk

=== final_talk ===
Estoy muy agradecido por tu apoyo hoy, fue importante y sobretodo me ayudó a ordenar mis pensamientos, me siento más calmado ahora. #title:Jorge
Qué bueno me alegro mucho que te sienta más en calma ¿Te sientes preparado para volver adentro? #title:Tú
Iré a buscar un vaso de agua y luego me iré a sentar, para ordenar mis tareas. Gracias nuevamente fue un apoyo muy importante #title:Jorge
#EndDialogue:End

    -> END