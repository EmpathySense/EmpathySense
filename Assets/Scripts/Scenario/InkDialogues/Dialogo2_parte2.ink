VAR correctAnswers = 0

VAR scoreSectionA = 0
VAR scoreSectionB = 0
VAR scoreSectionC = 0
VAR scoreSectionD = 0
VAR scoreSectionE = 0

VAR intentoA = 0
VAR intentoB = 0
VAR intentoC = 0
VAR intentoD = 0
VAR intentoE = 0

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
        ~ raise(intentoA)
    * ¿Qué te pasó?#dialog:guide 
        Recuerda que en un proceso de crisis la persona puede no tener claro qué es lo que le ocurre y preguntas como esta pueden generar más angustia.#dialog:normal #audio:feedback2_11 
        ~ raise(intentoA)
        ~ mistake_dialogue()
        ->five_choice
    * ¿Por qué estás llorando? #dialog:guide
        Recuerda evitar preguntas que puedan generar más angustia#dialog:normal #audio:feedback2_12
        ~ raise(intentoA)
        ~ mistake_dialogue()
        ->five_choice
    
- Bueno, estos últimos días he tenido muchas cosas en mente. #title:Jorge #dialog:guide 
- Recuerda que la persona que está en una crisis de pánico puede estar cerrada al diálogo, debes respetar lo que decida. #title:Consejo #dialog:normal
->six_choice


 === six_choice === 
 ¿Qué debería responder? #title:Tú
    * ¿Por qué crees que no eres valorado?#dialog:guide
        Recuerda evitar preguntas que puedan generar más angustia.#dialog:normal #audio:feedback2_13
        ~ raise(intentoA)
        ~ mistake_dialogue()
        ->six_choice
    * Entiendo, debe ser difícil para ti sentir eso.
        ~ raise(scoreSectionA)
        ~ raise(intentoA)
    * Ah mí me pasa igual, de hecho, el otro día fui hablar de esto con el jefe.
        ~ raise(intentoA)
        No me interesa tu conversación con el jefe.#title:Jorge#dialog:guide
        Este comentario resta validación a lo que comenta la otra persona, volviendo el foco hacia ti.#dialog:normal #audio:feedback2_14
        ->six_choice
    
- Si, porque tengo más trabajo del que puedo hacer, aparte cada fecha límite se pisa con la otra, no termino con una cuando ya tengo que empezar con otra tarea. #title:Jorge 
->seven_choice


 === seven_choice ===
 ¿Qué debería responder? #title:Tú
    * Comprendo lo que me cuentas, te refieres a sentirte con sobrecarga laboral
        ~ raise(scoreSectionA)
        ~ raise(intentoA)
    * Todos estamos igual de sobrepasados.
        Así te veo. #title:Jorge#dialog:guide
        Esta respuesta corre el riesgo de invalidar el sentir de la otra persona.#dialog:normal #audio:feedback2_15
        ~ raise(intentoA)
        ->seven_choice
    * Yo tengo un método que me funciona muy bien para organizar las tareas
        No creo que me sirva ahora tu método.#title:Jorge#dialog:guide
        Recuerda que lo importante aquí es el relato de la otra persona, es importante no protagonizar#dialog:normal #audio:feedback2_16
        ~ raise(intentoA)
        ->seven_choice
    
- Sí, de hecho llego a la casa y apenas duermo intentando terminar lo que me falta del día, me siento cansado y estoy lleno de ojeras. #title:Jorge
- Hasta sueño con el trabajo, luego por la mañana despierto para llenarme de café y seguir trabajando. #title:Jorge
->eight_choice

 === eight_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Dijiste que soñabas incluso con el trabajo?
        ~ raise(scoreSectionA)
        ~ raise(intentoA)
    * A veces nos tocan momentos difíciles#dialog:guide
        Este mensaje podría relativizar el dolor de la otra persona.#dialog:normal #audio:feedback2_17
        ~ raise(intentoA)
        ~ mistake_dialogue()
        ->eight_choice
    * No hay mal que por bien no venga[.], quizá tienes que pasar esto para que pasen otras cosas mejores 
        No es nada agradable que te hagan pasar por esto.#title:Jorge #dialog:guide
        Este mensaje podría relativizar el dolor de la otra persona#dialog:normal #audio:feedback2_18
        ~ raise(intentoA)
        ->eight_choice
    
- Sí, y eso es lo peor porque ni descansando me puedo librarme de él, lo que quiero es descansar, tomar vacaciones e irme un tiempo de acá, pero no puedo, porque no me dan vacaciones y sobretodo porque no tengo plata para hacerlo. #title:Jorge

- \(Habla un poco más rápido y está levemente agitado) #title:Jorge #animation:TrSittingDisbelief1

- Además me siento excluido acá, en el horario de colación o a la salida todos están con sus grupos y yo me quedo apartado. Creo que no les caigo bien. #title:Jorge
->nine_choice


 === nine_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Dices que te sientes excluido?
        ~ raise(scoreSectionA)
        ~ raise(correctAnswers)
    * A mí también me ha pasado que a veces he sentido esa soledad #dialog:guide
        Recuerda que lo importante aquí es el relato de la otra persona, es importante no protagonizar.#dialog:normal #audio:feedback2_19
        ~ raise(intentoA)
        ~ mistake_dialogue()
        ->nine_choice
    * Pero no te sientas mal[.], si ellos son los que se lo pierden, a mí me caes bien 
        Que alentador tu comentario muchas gracias \(tono sarcastico). #title:Jorge#dialog:guide
        Recuerda no buscar dar soluciones rápidas, esto puede cerrar el diálogo y no ser lo que requiere la persona#dialog:normal #audio:feedback2_20
        ~ raise(intentoA)
        ->nine_choice

-Sí, bueno quizá sea que no he tenido muchas oportunidades para que me conozcan. Después del trabajo no me quedan muchas energías para nada, me voy a la casa apenas salgo. ¡Estoy chato! #title:Jorge #animation:TrSittingDisbelief2
- \(Es notorio que se encuentra agitado) #title:Jorge
->ten_choice

/* PASO B  */


 === ten_choice ===
 ¿Qué debería responder? #title:Tú
    * Si me dices las cosas que te preocupan, podemos ordenarlas e ir mejorando la situación.#dialog:guide
        Recuerda no acelerar el proceso. Esta respuesta podría ser más cercana al tercer paso#dialog:normal #audio:feedback2_21
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->ten_choice
    * [Conozco un ejercicio de respiración] Mira, percibo que estás algo angustiado, yo conozco un ejercicio de respiración que me ha servido harto en este tipo de situaciones para encontrar calma, ¿te gustaría que lo probemos?  
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * Intenta respirar mejor para calmarte, fíjate como lo hago#dialog:guide
        Recuerda no entrar en un rol directivo #dialog:normal #audio:feedback2_22
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->ten_choice

-No creo que ayude. #title:Jorge
->eleven_choice


 === eleven_choice ===
 ¿Qué debería responder? #title:Tú
    * Tienes que hacer lo que yo hago, mírame bien #dialog:guide
        Recuerda no entrar en un rol directivo#dialog:normal #audio:feedback2_23
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->eleven_choice
    * ¿Qué te parece si lo intentamos? Yo te acompaño, y vemos si nos sirve.   
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * \(Comenzar a respirar esperando que te siga) #dialog:guide
        Recuerda que es importante explicitar lo que harán en los siguientes momentos. Anticipar es clave para recuperar la tranquilidad#dialog:normal #audio:feedback2_24
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->eleven_choice

-\(Asiente) Está bien. #title:Jorge
->twelve_choice


 === twelve_choice ===
 ¿Qué debería responder? #title:Tú
    * Quizás has escuchado lo de respirar consciente y profundo.[] En realidad, sirve bastante, mira te voy a explicar cómo podemos hacerlo  
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * Tienes que hacer la respiración de 4 tiempos, con eso estás listo #dialog:guide 
        Recuerda que es importante explicitar lo que harán en los siguientes momentos. Anticipar es clave para recuperar la tranquilidad#dialog:normal #audio:feedback2_25
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->twelve_choice

-\(Asiente) #title:Jorge #animation:TrSittingLookDown_1
->thirteen_choice


=== thirteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Haremos la técnica de respiración de 4 tiempos[.], primero tomas aire en 4 tiempos, luego botas el aire en 4 tiempos y finalmente hay que mantener los pulmones "vacíos" por 4 tiempos   
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * Es fácil, es inhalar, exhalar y mantener sin aire. #dialog:guide
        Recuerda que es importante explicar con calma y paciencia, para que la persona pueda anticiparse.#dialog:normal #audio:feedback2_26
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->thirteen_choice
    * Mira obsérvame primero, en lo que hago y entenderás mejor. #dialog:guide
        Recuerda que es importante explicar con calma y paciencia, para que la persona pueda anticiparse#dialog:normal #audio:feedback2_27
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->thirteen_choice

-\(Asiente) #title:Jorge
->fourteen_choice

=== fourteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Intentémoslo, hazlo contando a un ritmo que te sea cómodo.[] Inhala 1, 2, 3, 4, exhala 1, 2, 3, 4, ahora mantén 1, 2, 3, 4 #dialog:guide
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * Veamos si te sale a la primera o no.#dialog:guide
        Recuerda no generar experiencias más angustiantes.#dialog:normal #audio:feedback2_28
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->fourteen_choice
    * Tienes que hacerlo bien, o si no te vas a agitar más.#dialog:guide
        Recuerda que tus palabras deben invitar a la calma.#dialog:normal #audio:feedback2_29
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->fourteen_choice 
    

- Recuerda que los 4 tiempos no son necesariamente 4 segundos, puede variar según el estado de agitación de la persona #title:Consejo #dialog:normal #audio:feedback2_medio1
- \(Mejora poco a poco la respiración)  #title:Jorge
->fiveteen_choice

=== fiveteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Lo estás haciendo muy bien, sigamos repitiendo el ejercicio  
        ~ raise(intentoB)
        ~ raise(correctAnswers)
    * \¡Bien Jorge! No esperaba que te saliera a la primera, pero lo hiciste súper bien.#dialog:guide
        Recuerda evitar comentario que sean juicios.#dialog:normal #audio:feedback2_30
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->fiveteen_choice 
    * Qué bueno que ahora estás mejor, ¿qué te había pasado? #dialog:guide 
        Si has logrado que la persona se pueda calmar es importante promover el mantener ese estado. Esta pregunta podría volver la agitación.#dialog:normal #audio:feedback2_31
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->fiveteen_choice 

-\(Asiente) #title:Jorge #animation:TrSittingMovingLegs
- Vamos de nuevo, inhala 1, 2, 3, 4, exhala 1, 2, 3, 4, ahora mantén 1, 2, 3, 4 #title:Tú #dialog:guide
- Recuerda que este paso dura aproximadamente 10 minutos, pudiendo incluso tardar más, debes asegurarte de que la persona recuperó la calma. #title:Consejo #dialog:normal #audio:feedback2_medio2
- \(respira normal, notablemente más calmado)
->sixteen_choice

=== sixteen_choice ===
 ¿Qué debería responder? #title:Tú
    * ¿Qué tal? ¿Te sientes un poco más calmado?  
        ~ raise(scoreSectionB)
        ~ raise(intentoB)
    * Que bien que estás más calmado, pensé que íbamos a estar más tiempo con eso
        Perdon por usar tu tiempo.#dialog:guide
        Recuerda evitar comentario que sean juicios.#dialog:normal #audio:feedback2_32
        ~ raise(intentoB)
        ->sixteen_choice 
    * Viste si era fácil, hasta tú pudiste hacerlo #dialog:guide
        Recuerda evitar comentario que sean juicios.#dialog:normal #audio:feedback2_32
        ~ raise(intentoB)
        ~ mistake_dialogue()
        ->sixteen_choice 

-Si, gracias, ahora me siento un poco mejor. #title:Jorge

->seventeen_choice

=== seventeen_choice ===
 ¿Qué debería responder? #title:Tú
    * Este es un muy buen ejercicio [, puedes aplicarlo en varias situaciones], puedes aplicarlo en esta situación o alguna parecida donde creas perder la calma. También vi que recomiendan practicarlo al despertar o antes de dormir, para mejorar sus resultados.  
        ~ raise(scoreSectionC)
        ~ raise(intentoC)
    * Menos mal estaba cerca, quizá que hubiese pasado, ¡pudiste desmayarte!
        No digas eso que me preocupo.#dialog:guide
        Recuerda no generar comentarios que puedan angustiar más a la persona.#dialog:normal #audio:feedback2_33
        ~ raise(intentoC)
        ->seventeen_choice
    * Es bueno este ejercicio, para la otra ya sabes que hacer, ¡no se te vaya a olvidar! #dialog:guide
        Recuerda que lo importante es reforzar la tranquilidad actual de la persona.#dialog:normal #audio:feedback2_34
        ~ raise(intentoC)
        ~ mistake_dialogue()
        ->seventeen_choice 

-Gracias, lo tendré en cuenta. Estos días me haría muy bien al dormir sobretodo. #title:Jorge #animation:TrSittingTalking

->eighteen_choice

/* PASO C  */


=== eighteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Vamos de vuelta a la oficina y vemos si puedes volver a trabajar
        De momento dejame solo si estas apurado te puedes ir.#dialog:guide
        No olvides que es importante incluir en las decisiones a la persona afectada, no tomar un rol directivo.#dialog:normal #audio:feedback2_35
        ~ raise(intentoC)
        ->eighteen_choice
    * Yo creo que podrías hablar con un terapeuta o alguien profesional
        No tengo tiempo para esto.#dialog:guide
        Recuerda no dar consejos que no han sido solicitados.#dialog:normal #audio:feedback2_36
        ~ raise(intentoC)
        ->eighteen_choice
    * [¿Qué sientes que es lo más necesitas resolver ahora?] Jorge, ahora que te sientes más tranquilo y retomando lo que me contaste, ¿Qué sientes que es lo más necesitas resolver ahora? 
        ~ raise(scoreSectionC)
        ~ raise(intentoC)
-El tema del trabajo, es que últimamente es lo único que hago, tengo tanto que hacer y todo acumulado, que ya ni sé por dónde empezar, solo me estreso de pensarlo. #title:Jorge
->nineteen_choice

=== nineteen_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo creo que tienes que tomar licencia un par de días o semanas y vuelves renovado. 
        No le veo beneficioso para todo lo que tengo que hacer.#dialog:guide
        Recuerda no dar consejos que no han sido solicitados.#dialog:normal #audio:feedback2_37
        ~ raise(intentoC)
        ->nineteen_choice
    * Es que te falta organización, quizá dejas mucho tiempo muerto
        Perdon sabio de la organización.#dialog:guide
        Recuerda no hacer comentarios que se enmarquen en juicios.#dialog:normal #audio:feedback2_38
        ~ raise(intentoC)
        ->nineteen_choice
    * Entonces en lo que dices lo más importante de abordar ahora para ti es el trabajo, en relación al tiempo y la sobrecarga 
        ~ raise(scoreSectionC)
        ~ raise(intentoC)

-Si es que antes yo sólo trabajaba media jornada, y tenía un poco más tiempo libre, ahí iba al cine o salía con excompañeros de la universidad, creo que eso me alivianaba un poco la carga que tenía. #title:Jorge

->twenty_choice

=== twenty_choice ===
 ¿Qué debería responder? #title:Tú
    * Comprendo, y como decías ahora tienes poco tiempo y te es difícil hacer esas actividades. 
        ~ raise(scoreSectionC)
        ~ raise(intentoC)
    * Pero tómate un día de estos para salir o hacer algo, no creo que pase nada acá, el jefe ni se fija cuando falta alguien.
        Claro, suena muy fácil hacerlo no cuando tienes tantas cosas que hacer.#dialog:guide
        Recuerda no dar consejos que no han sido solicitados. Esta respuesta podría generar más angustia al incorporar nuevos escenarios.#dialog:normal #audio:feedback2_39
        ~ raise(intentoC)
        ->twenty_choice
    * Ahora estamos mayores, nos toca dejar algunas cosas de lado.
        No es tan fácil.#dialog:guide
        Recuerda no protagonizar ni hacer comentarios teñidos de juicio.#dialog:normal #audio:feedback2_40
        ~ raise(intentoC)
        ->twenty_choice

-Claro, además de que tengo poco tiempo, tengo poca plata, después de pagar el arriendo tengo que estirar lo que me queda para pasar el mes, todo está súper caro. #title:Jorge

->twenty_one_choice

=== twenty_one_choice ===
 ¿Qué debería responder? #title:Tú
    * [Ordenando lo que me comentaste...] Entonces, ordenando lo que me comentaste, me decías que tienes mucho trabajo, poco tiempo, y estás muy justo con el dinero que ganas ¿Y de esto, que crees que es lo que más te aliviaría resolver primero? 
        ~ raise(scoreSectionC)
        ~ raise(intentoC)
    * Si necesitas plata yo te puedo prestar, ya que tengo casa no pago arriendo.
        No es un tema de plata.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona en encontrar su propia estabilidad.#dialog:normal #audio:feedback2_41
        ~ raise(intentoC)
        ->twenty_one_choice
    * Podrías cambiarte a algún lugar que te salga más barato, como vives solo no creo que necesites mucho espacio.#dialog:guide
        Recuerda no realizar comentarios juiciosos ni dar consejos no solicitados.#dialog:normal #audio:feedback2_42
        ~ raise(intentoC)
        ~ mistake_dialogue()
        ->twenty_one_choice

-Yo creo que la carga laboral, con eso organizado mejor creo poder manejar el resto. #title:Jorge

->twenty_two_choice

=== twenty_two_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo te puedo ayudar con eso, voy a hablar con el jefe y que te dé más plazo para entregar lo que te falta. 
        Ya sabes como es no le importa nuestra estado.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona.#dialog:normal #audio:feedback2_43
        ~ raise(intentoC)
        ->twenty_two_choice
    * Yo hago lo que te falte, soy rápido con eso, así quedas más libre#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona en encontrar su propia estabilidad.#dialog:normal #audio:feedback2_44
        ~ raise(intentoC)
        ~ mistake_dialogue()
        ->twenty_two_choice
    * ¿Hay alguna forma en la que sientes que podrías atender eso o recibir ayuda al respecto?
        ~ raise(scoreSectionC)
        ~ raise(intentoC)

-No lo sé, no he pensado en muchas soluciones #title:Jorge

->twenty_three_choice


/* PASO D  */


=== twenty_three_choice ===
 ¿Qué debería responder? #title:Tú
    * Sé que a veces no es fácil lidiar con esto por tu cuenta. ¿Has considerado la posibilidad de hablar con un terapeuta o psicólogo?  
        ~ raise(scoreSectionD)
        ~ raise(intentoD)
    * Voy a ir a recursos humanos para que pueda hablar con el resto del trabajo y que sepan tu situación
        Esos buenos para nada nunca nos han ayudado.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona.#dialog:normal #audio:feedback2_45
        ~ raise(intentoD)
        ->twenty_three_choice
    * Mira pásame tu celular, podemos llamar a algún familiar para que sepa lo que te pasó y ver si te puede ayudar.
        No quiero molestar a nadie.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona e incluso angustiarla.#dialog:normal #audio:feedback2_46
        ~ raise(intentoD)
        ->twenty_three_choice

-No sé, es primera vez que me pasa esto, no creo que sea necesario. #title:Jorge

->twenty_four_choice

=== twenty_four_choice ===
 ¿Qué debería responder? #title:Tú
    * Yo creo que tienes que ir al psicólogo, podría ser la primera de muchas
        No confio mucho.#dialog:guide
        Recuerda no dar consejos no solicitados sobre todo si implícitamente tienen un mensaje que puede angustiar a la persona sobre el futuro.#dialog:normal #audio:feedback2_47
        ~ raise(intentoD)
        ->twenty_four_choice
    * Entiendo que quizás desconfíes de ese tipo de apoyo[.], pero si más adelante cambias de opinión, si quieres te puedo entregar algunos datos.
        ~ raise(scoreSectionD)
        ~ raise(intentoD)
    * Es importante que vayas sí o sí a ver un profesional.
        ¿Tan mal crees que estoy?#dialog:guide
        Recuerda no dar consejos no solicitados.#dialog:normal #audio:feedback2_48
        ~ raise(intentoD)
        ->twenty_four_choice
-Bueno, quien sabe. #title:Jorge

->twenty_five_choice

=== twenty_five_choice ===
 ¿Qué debería responder? #title:Tú
    * Acá en el trabajo puedes hablar conmigo cuando necesites.
        No te preocupes, no te quiero quitar de tu tiempo.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona.#dialog:normal #audio:feedback2_49
        ~ raise(intentoD)
        ->twenty_five_choice
    * Sé que la empresa tiene varios convenios[.] con diversos lugares donde puedes ir, entras a intranet y pones convenios, ahí puedes ver la información con más detalle y el contacto de cada uno.
        ~ raise(scoreSectionD)
        ~ raise(intentoD)
    * Ahora no me acuerdo bien, pero buscas en Google y te aparecen varios lugares.
        Y como sabre cual de todos esos es la mejor decisión para mi situación.#dialog:guide
        Recuerda la importancia de entregar información oportuna a la persona para que pueda tomar decisiones.#dialog:normal #audio:feedback2_50
        ~ raise(intentoD)
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
        ~ raise(intentoE)
    * Si quieres guarda mi número y podemos conversar, generalmente respondo rápido y estoy pendiente.
        Bueno, pero no quiero ser molestia constante.#dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona.#dialog:normal #audio:feedback2_51
        ~ raise(intentoE)
        ->twenty_six_choice
    * Lo hiciste súper hoy, ya está listo para una próxima vez.
        ¿Tan pronto me puede pasar de nuevo?#dialog:guide
        Recuerda que por más que quieras dar un refuerzo positivo e importante no entregar con ello un mensaje que pueda generar angustia hacia el futuro.#dialog:normal #audio:feedback2_52
        ~ raise(intentoE)
        ->twenty_six_choice
-\(Asiente) Bueno, ¿Cómo el ejercicio de respiración? #title:Jorge
->twenty_seven_choice

=== twenty_seven_choice ===
 ¿Qué debería responder? #title:Tú
    * [Sí, ademas hay otras estrategias como...] Exacto, puedes aplicarlo cuando te sientas un poco agitado o estresado, también hay otras estrategias que se ha visto que sirven harto, como mantener horarios de descansos regulares y compartir con amigos o familiares  
        ~ raise(scoreSectionE)
        ~ raise(intentoE)
    * Claro, si estás estresado recuerda lo que te enseñé y eso te va a servir para todo
        Pero en el momento podría estar pensando muchas cosas y no saber que hacer.#dialog:guide
        Recuerda no protagonizar y reforzar el aprendizaje independiente y regulación autónoma.#dialog:normal #audio:feedback2_53
        ~ raise(intentoE)
        ->twenty_seven_choice
    * Hay muchas más cosas que te pueden ayudar, pero ya tienes que ver tú que te sirve#dialog:guide
        Recuerda que lo importante es reforzar lo aprendido para ir cerrando el acompañamiento.#dialog:normal #audio:feedback2_54
        ~ mistake_dialogue()
        ~ raise(intentoE)
        ->twenty_seven_choice
-Me siento mejor sabiendo que es más normal de lo que pensaba.  #title:Jorge
->final_talk

=== final_talk ===
Estoy muy agradecido por tu apoyo hoy, fue importante y sobretodo me ayudó a ordenar mis pensamientos, me siento más calmado ahora. #title:Jorge
Qué bueno me alegro mucho que te sienta más en calma ¿Te sientes preparado para volver adentro? #title:Tú
Iré a buscar un vaso de agua y luego me iré a sentar, para ordenar mis tareas. Gracias nuevamente fue un apoyo muy importante #title:Jorge
#EndDialogue:End
-> END