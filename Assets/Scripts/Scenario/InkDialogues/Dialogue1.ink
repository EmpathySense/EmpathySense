 VAR correctAnswers = 0
 VAR mistakes = 0
 
 
 ->start
 
 /*--------------------------------------------------------------------------------
	Wrap up character movement using functions, in case we want to develop this logic in future
--------------------------------------------------------------------------------*/


 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1



=== start ===
Vienes saliedo de la universidad, vas caminando por la vereda camino a casa cuando notas que en una plaza está uno de tus amigos, Ignacio. #title:Contexto
Está sentado en una banca mirando al piso y con las manos en la cabeza, tratas de llamar su atención pero no se da cuenta de que estás ahí.
Decides acercarte a él para saludarlo.
Hola. #title:Tú
Oh, hola. Ne me había dado cuenta de que estabas ahí #title:Ignacio #animation:TrSittingTalking
Oye, ¿Estás bien? #title:Tú
Eh... Bueno... No, la verdad es que no. #title:Ignacio
Recién tuve una prueba de matemáticas y no me fue muy bien.
Estuve estudiando mucho tiempo y aun así me quedé bloqueado, ha sido así en todos los ramos que estoy dando, no importa cuánto estudie, nunca es suficiente. #animation:TrSittingLookDown
 No sé qué debería hacer, creo que he intentado hasta las técnicas de estudio de tiktok y nada sirve para concentrarme.
 ->first_choice
 
 = first_choice
 ¿Qué debería responder? #title:Tú
    * ¿Has Probado la técnica del pomodoro?
        Sí, ya te dije, lo intenté absolutamente todo #title:Ignacio 
        ~ raise(mistakes)
        ->first_choice
    * Aún te quedan más pruebas, puedes hacerlo
        Realmente lo dudo, así como voy estoy necesitando un 8 en cada prueba que queda para poder optar a pasar. #title:Ignacio
        ~ raise(mistakes)
        ->first_choice
    * ¿Por qué crees que no puedes concentrarte?
        ~ raise(correctAnswers)
        ->cant_concentrate
        
= cant_concentrate
Es... porque cada vez que tengo que estudiar... Mi familia pide que haga cosas o me interrumpe en medio de mi estudio... #title:Ignacio #animation:TrSittingTalking_2
Lo peor es que si les digo que no puedo hacer lo que me piden se enojan conmigo. Ni me imagino como irán a reaccionar cuando se enteren que me está yendo mal en la u.
->second_choice

= second_choice
¿Qué debería responder? #title:Tú
    *  Deberías conversarlo con ellos, de más que te van a entender
    ~ raise(mistakes)
        Ellos jamás me van a escuchar, si intentara hablarles sobre eso creerían que les estoy faltando el respeto o algo así. #title:Ignacio 
        -> second_choice
    * ¿Tus papás son muy estrictos?
        ~raise(correctAnswers)
        ->talk_about_parents
    * Mis papás también son medio pesados, pero consigo manejarme con ellos. 
        ~raise(mistakes)
        (En un tono sarcástico) Vaya, que bueno que tus papás sean capaces de darte un poco de espacio para que puedas hacer tus cosas. Tu vida suena mucho mejor que la mía.  #title:Ignacio
        Disculpa, no quería ofenderte. #title:Tú
        (Suspira) Si, si... claro, entiendo #title:Ignacio
        -> second_choice

= talk_about_parents
Mis papás en general tienen mal genio, desde el momento en que llegan a la casa no hacen más que pelear entre ellos, con mi hermana y conmigo. #title:Ignacio #animation:TrSittingLookDown_2
Es como si se esforzaran por asegurarse de que todo el mundo los escuche...
(Levantando la voz) Por eso tampoco me puedo concentrar, siempre hay un montón de ruido. Y no es como si pudiera quedarme a estudiar en la universidad tampoco, porque si llego muy tarde también es un problema para ellos... #animation:TrSittingDisbelief2
(Sollozando) Ya no sé qué hacer... Encima que la próxima semana se viene otro montón de evaluaciones y ya pasó el plazo para des inscribir los ramos, no sé por dónde partir estudiando ni dónde estudiar. 
¿Qué debería responder? #title:Tú
    * \(No decir nada\)
    * Podría ser peor...
        ~raise(mistakes)
    * Tranquilo, vas a estar bien
        ~raise(mistakes)
- Comienzas a notar que Ignacio empieza a respirar más rápido mientras le siguen cayendo lágrimas. Con las manos temblorosas intenta fuertemente secarse las lágrimas.
    ->third_choice

= third_choice
¿Qué debería hacer? #title:Tú
    * ¿Quieres que llame a alguien?
        ~raise(mistakes)
        
    * No llores, estoy seguro de que puedes hablar con tus papás. 
        ~raise(mistakes)
        
    * Necesitas respirar más despacio, por favor, déjame ayudarte.
        ~raise(correctAnswers)
-\(No responde) #title:ignacio
Mira, vi este video en youtube que decía que regular la respiración ayudaba a las personas a calmarse. #title:Tú
Creo que tiene que ver con la cantidad de oxígeno que hay en nuestro cerebro y cómo esto afecta a nuestras emociones. Puedo enseñártelo si quieres. 
(Asiente con la cabeza) #title:Ignacio
->fourth_choice

= fourth_choice
¿Qué debería decir? #title:Tú
    * Ok, primero debes sentarte en una posición cómoda
        ~raise(correctAnswers)
    * Ok, pero primero debes dejar de llorar 
        ~raise(mistakes)
        (Espera, eso no tiene sentido)
        ->fourth_choice
    * Ok, primero debes pensar en algo feliz 
        ~raise(mistakes)
        (Espera, eso no tiene sentido)
        ->fourth_choice
- \(Se acomoda en la banca, sentándose en una posición recta) #title:Ignacio #animation:TrSittingMovingLegs
->fifth_choice

= fifth_choice
¿Qué venía ahora? #title:Tú
    * Ok, tienes que inhalar y contar hasta 4
        ~raise(correctAnswers)
    * Ok, inhala hasta que yo te avise
        ~raise(mistakes)
    * Ok, inhala hasta que tengas los pulmones llenos
        ~raise(mistakes)
-¿Qué venía ahora?
    
	* Después, exhala hasta que yo te avise 
	    ~raise(mistakes)
    * Después, exhala contando hasta 4 
        ~raise(correctAnswers)
	* Después, exhala hasta que tengas los pulmones vacíos 
	    ~raise(mistakes)
-¿Qué venía ahora?
    * Ahora repite 
        ~raise(mistakes)
	* Ahora, cuenta hasta 4 con los pulmones vacíos y luego repite 
	    ~raise(correctAnswers)
- \(Repite las indicaciones que le diste) #title:Ignacio
¿Qué debería hacer? #title:Tú
    * Hagámoslo juntos(Imita los pasos de respiración junto a Ignacio) 
        ~raise(correctAnswers)
    * Bien hecho, sigue así
        ~raise(correctAnswers)
-\(Pasan unos cuantos minutos...) #title:Contexto
¿Te sientes mejor? #title:Tú
Creo que sí. Gracias. #title:Ignacio #animation:TrSittingTalking_3
->sixth_choice

= sixth_choice
¿Qué debería hacer? #title:Tú
    * ¿Seguro?
        Sí, estoy seguro. #title:Ignacio
        
    * No deberías alterarte así de rápido, deberías buscar soluciones primero.
        ~raise(mistakes)
        Disculpa, no quise molestarte, pero realmente estoy muy perdido. #title:Ignacio
         No te preocupes, está bien, le puede pasar a cualquiera. #title:Tú
        ->sixth_choice
    * ¿Crees que podamos retomar un poco de lo que me estabas diciendo antes?
        ~raise(correctAnswers)
        Si... Ahora estoy un poco más tranquilo. #title:Ignaico
- ¿Qué debería hacer? #title:Tú
    * ¿Crees que puedas decirme cuál es el problema principal?
        Bueno... si tuviera que decir yo... creo que más que nada necesito un lugar donde poder estudiar.#title:Ignacio
        ~raise(correctAnswers)

    * Según lo que estabas diciendo antes, creo que el principal problema es que necesitas un lugar donde puedas estudiar.
        Si, creo que sí. Pero no sé qué hacer respecto a eso.#title:Ignacio
        ~raise(correctAnswers)

- Además está el problema de mis papás, no creo que pueda convencerlos de nada.

¿Qué debería hacer? #title:Tú
    *  No es necesario que hables con ellos solo, puedes pedirle a tu hermana que te acompañe a o quizás a un amigo que los conozca mejor que yo.
        ~raise(correctAnswers)
        No había considerado eso, ella también tiene muchos problemas con ellos. #title:Ignacio
    * Entiendo, en ese caso creo que podrías intentar pensar en una solución más adelante.
- Si necesitas un lugar donde estudiar, puedes venir a estudiar a mi casa. Mis papás no tendrían ningún problema con eso.#title:Tú
Además, tenemos varios ramos parecidos, por lo que sería beneficioso para ambos. 
No lo sé... no quiero molestarte. #title:Ignacio
No te preocupes, no vivimos tan lejos uno del otro, por lo que llegar a tu casa a una hora “aceptable” no sería un problema, #title:Tú
Y si realmente quieres llegar rápido, le puedo pedir el auto a mi mamá y te puedo dejar en tu casa yo mismo. 
¿En serio? #title:Ignacio
Sí ¿Por qué no? #title:Tú
No tienes por qué pasar por estas dificultades tú solo, puedes hablar de tus problemas conmigo o con cualquiera de nuestros amigos.
Siempre vamos a intentar ayudarte como podamos. 
Gracias, es bueno escuchar eso. #title:Ignacio
Además, siempre puedes pedir hora en el psicólogo de nuestra universidad, es libre de costo y no tiene nada de malo. #title:Tú
Ellos te pueden dar ayuda mucho mejor para tus problemas. 
Lo tendré en consideración. #title:Ignacio
->END
