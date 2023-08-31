VAR correctAnswers = 0

VAR scoreSectionA = 0
VAR scoreSectionB = 0
VAR scoreSectionC = 0
VAR scoreSectionD = 0
VAR scoreSectionE = 0

VAR mistakes = 0


-> start


 === function lower(ref x)
 	~ x = x - 1

 === function raise(ref x)
 	~ x = x + 1
 	
 	

/**
 * Esta es la segunda versión del dialogo 1.
 * La diferencia es que este tiene todas las
 * correcciones hechas por Scarlet
*/

=== start ===
Vienes saliendo de la universidad, vas caminando por la vereda camino a casa cuando notas que en una plaza está uno de tus amigos. #title:Contexto
Está sentado en una banca mirando al piso, con las manos en la cabeza y llorando efusivamente. Tratas de llamar su atención, pero no se da cuenta de que estás ahí.
Decides acercarte a él para saludarlo.
Hola #title:Tú
Oh, hola. No me había dado cuenta de que estabas ahí. #title:Ignacio
No quiero ser invasivo, pero veo que no te sientes muy bien ¿Me puedo sentar aquí al lado? #title:Tú
Sí. #title:Ignacio
No sé si tienes ganas de conversar, pero si quisieras yo te puedo escuchar. #title:Tú
Eh... Bueno... Recién tuve una prueba de matemáticas y no me fue muy bien. #title:Ignacio
Estuve estudiando mucho tiempo y aun así me quedé bloqueado, ha sido así en todos los ramos que estoy dando, no importa cuánto estudie, nunca es suficiente.
No sé qué debería hacer, creo que he intentado hasta las técnicas de estudio de tiktok y nada sirve para concentrarme.
->first_choice

=== first_choice ===
¿Qué debería responder? #title:Tú
    * Has probado la técnica del Pomodoro?
        Sí, ya te dije, lo intenté absolutamente todo #title:Ignacio
        ~raise(mistakes)
        ->first_choice
    * Aún te quedan más pruebas, puedes hacerlo 
        Realmente lo dudo, así como voy estoy necesitando un 8 en cada prueba que queda para poder optar a pasar. #title:Ignacio
        ~raise(mistakes)
        ->first_choice
    * Te entiendo, es muy difícil el intentarlo y no poder concentrarse
        ~raise(correctAnswers)
        ->talk_about_parents
        
=== talk_about_parents ===
Es que... cada vez que tengo que estudiar... Mi familia pide que haga cosas o me interrumpe en medio de mi estudio... #title:Ignacio
Lo peor es que si les digo que no puedo hacer lo que me piden se enojan conmigo. 
Ni me imagino como irán a reaccionar cuando se enteren que me está yendo mal en la u. 
->second_choice

=== second_choice ===
    * Deberías conversarlo con ellos, de más que te van a entender 
        Dices eso porque no los conoces, ellos jamás me van a escuchar, si intentara hablarles sobre eso creerían que les estoy faltando el respeto o algo #title:Ignacio
        ~raise(mistakes)
        ->second_choice
    * Y dentro de lo que me contaste ¿Piensas que ellos no entienden que estás ocupado cuando estudias?
        ~raise(correctAnswers)
        ->talk_about_parents_cont
    * Mis papás también son medio pesados, pero consigo manejarme con ellos.
        (En un tono sarcástico) Vaya, que bueno que tus papás sean capaces de darte un poco de espacio para que puedas hacer tus cosas. Tu vida probablemente es mucho mejor que la mía.  #title:Ignacio
        Disculpa, no quería ofenderte. #title:Tú
        (Suspira) Si, si... claro, entiendo #title:Ignacio
        ~raise(mistakes)
        ->second_choice
        
=== talk_about_parents_cont ===
Es que creen que no estudio, que estoy jugando. #title:Ignacio
Mis papás en general tienen mal genio, desde el momento en que llegan a la casa no hacen más que pelear entre ellos, con mi hermana y conmigo.
Es como si se esforzaran por asegurarse de que todo el mundo los escuche... 
(Levantando la voz) Por eso tampoco me puedo concentrar, siempre hay un montón de ruido.
Y no es como si pudiera quedarme a estudiar en la universidad tampoco, porque si llego muy tarde también es un problema para ellos...
(Sollozando) Ya no sé qué hacer...
Encima que la próxima semana se viene otro montón de evaluaciones y ya pasó el plazo para des inscribir los ramos, no sé por dónde partir estudiando ni dónde estudiar. 
->third_choice


=== third_choice ===
¿Qué debería responder? #title:Tú
    * Entiendo que te puedes sentir sobrepasado 
        (Guardas silencio)
        ~raise(correctAnswers)
    * Podría ser peor...
        Eso no me ayuda en nada #title:Igancio
        (Llora fuertemente)
        ~raise(mistakes)
    * Tranquilo, vas a estar bien.
- Comienzas a notar que el Ignacio empieza a respirar más rápido mientras le siguen cayendo lágrimas. Con las manos temblorosas intenta fuertemente secarse la cara. #title:Contexto
->help_breathing

//**** SEGUNDO PASO *****//

=== help_breathing ===
¿Qué debería responder? #title:Tú
    * ¿Quieres que llame a alguien?
        ~raise(mistakes)
    * No llores, estoy seguro de que puedes hablar con tus papás.
        ~raise(mistakes)
    * ¿Te gustaría que hiciéramos algo que te pueda entregar un poco de tranquilidad ahora?
        ~raise(correctAnswers)
-\(No responde) #title:Ignacio
Mira, vi este video en youtube que decía que regular la respiración ayudaba a las personas a calmarse. #title:Tú
En el video explican que al respirar mejor y más profundo nuestras emociones se pueden regular. Puedo enseñártelo si quieres. 
(Asiente con la cabeza) #title:Ignacio
->breathing_1

=== breathing_1 ===
¿Qué debería decir? #title:Tú
    * Ok, primero nos vamos a sentar en una posición cómoda.
        ~raise(correctAnswers)
    * Ok, pero primero debes dejar de llorar.
        (Continúa llorando)  #title:Ignacio
        ~raise(mistakes)
        ->breathing_1
    * Ok, primero debes pensar en algo 
        (Te mira con una cara de confusión y de rechazo) #title:Ignacio
        ->breathing_1
-\(Se acomoda en la banca, sentándose en una posición recta)
->breathing_2

=== breathing_2 ===
¿Qué debería decir? #title:Tú
    * Te lo voy a explicar en general primero, lo que haremos será respirar en 4 tiempos, luego botar el aire en 4 tiempos y luego esperar 7 tiempos para volver a respirar
        ~raise(correctAnswers)
    * Cópiame como respiro
        (Espera, eso no tiene sentido)
        ~raise(mistakes)
        ->breathing_2
    *Sólo respira como si quisieras inflar tu estómago
        (Espera, eso no tiene sentido)
        ~raise(mistakes)
        ->breathing_2
-\(Asiente con la cabeza) #title:Ignacio
->breathing_3

=== breathing_3 ===

¿Qué debería decir? #title:Tú
    *Ok, hagamos la primera parte. Tienes que inhalar y contar hasta 4, a tu ritmo
        ~raise(correctAnswers)    
    *Ok, inhala hasta que yo te avise
        ~raise(mistakes)
    *Ok, inhala hasta que tengas los pulmones llenos.
        ~raise(mistakes)
-\(Respira) #title:Ignacio
->breathing_4

=== breathing_4 ===

¿Qué debería decir? #title:Tú
    *Ahora, exhala contando hasta 4, igualmente a tu ritmo
        ~raise(correctAnswers)    
    *Después, exhala hasta que yo te avise 
        ~raise(mistakes)
    *Después, exhala hasta que tengas los pulmones vacíos  
        ~raise(mistakes)
-\(Respira) #title:Ignacio
->breathing_5

=== breathing_5 ===

¿Qué debería decir? #title:Tú
    *Ahora, cuenta 4 tiempos antes de volver a inhalar
        ~raise(correctAnswers)    
    *Ahora aguante 4 minutos  
        ~raise(mistakes)
    *Ahora vuelve a inhalar
        ~raise(mistakes)
-\(Respira) #title:Ignacio
->breathing_6

=== breathing_6 ===

¿Qué debería decir? #title:Tú
    *Ahora repite 
        ~raise(correctAnswers)    
    *Ahora, cuenta hasta 4 con los pulmones vacíos y luego repite   
        ~raise(correctAnswers)

-\(Repite las indicaciones que le diste) #title:Ignacio
\(Imita los pasos de respiración junto al afectado)#title:Tú
Bien hecho, sigue así#title:Tú
->after_breathing

=== after_breathing ===

Después de un minuto haciendo la técnica de respiración, el AFECTADO ha conseguido respirar con calma. 
¿Te sientes mejor? #title:Tú
Creo que si, o sea no bien completo, pero puedo hablar mejor. Gracias #title:Ignacio
->fourth_choice

//**** TERCER PASO *****//

=== fourth_choice ===
¿Qué debería decir? #title:Tú
    *¿Seguro?
    Si, estoy seguro. #title:Ignacio
    No deberías alterarte así de rápido, deberías buscar soluciones primero #title:Tú
    Disculpa, no quise molestarte, pero realmente estoy muy perdido #title:Ignacio
    No te preocupes, está bien, le puede pasar a cualquiera. #title:Tú
    ~raise(mistakes)
    ->fourth_choice
    *¿Crees que podamos retomar un poco de lo que me estabas diciendo antes?
    Si... Ahora estoy un poco más tranquilo.  #title:Ignacio
    ~raise(correctAnswers)
    ->five_choice
    
=== five_choice ===
¿Qué debería decir? #title:Tú
    *De lo que me contaste antes ¿Qué sientes que es lo más problemático para ti hoy? 
    Bueno... si tuviera que decir yo... creo que más que nada necesito un lugar donde poder estudiar. #title:Ignacio
    ~raise(correctAnswers)
    ->sixth_choice
    *Realmente creo que debes hablar con tus papás al respecto.
    Sería una pérdida de tiempo#title:Ignacio
    ~raise(mistakes)
    ->five_choice
=== sixth_choice ===
¿Qué debería decir? #title:Tú
    *Entonces piensas que tú prioridad en este momento es cubrir la necesidad de un lugar para estudiar tranquilo
    Si, creo que sí. Pero no sé qué hacer respecto a eso. #title:Ignacio
    Además está el problema de mis papás, no creo que pueda convencerlos de nada. #title:Ignacio
    ~raise(correctAnswers)
    ->talk_about_study
    *No es necesario que hables con ellos solo, puedes pedirle a tu hermana que te acompañe a conversar
    No había considerado eso, ella también tiene muchos problemas con ellos. #title:Ignacio
    ~raise(mistakes)
    ->talk_about_study
    
=== talk_about_study ===
Entiendo. Mira a veces cuando estamos abrumados sirve ordenar nuestras prioridades para no angustiarnos por intentar resolver todo a la vez #title:Tú
\(Asiente con la cabeza) #title:Ignacio
¿Crees que sería importante resolver entonces primero lo del espacio, que fue lo que mencionaste antes? #title:Tú
Lo del espacio de estudio es lo más urgente en verdad, porque estoy en un momento crítico académico #title:Ignacio
->seven_choice

//**** CUARTO PASO *****//

=== seven_choice ===
¿Qué debería decir? #title:Tú
    *Si necesitas un lugar donde estudiar, puedes venir a estudiar a mi casa, probablemente tenemos los mismos ritmos de estudio 
    No lo sé... no quiero molestarte. #title:Ignacio
    ~raise(mistakes)
    ->seven_choice
    *Voy a llamar a la Universidad para que te ayuden 
    No, por favor no! Necesito ver qué haré antes #title:Ignacio
    ~raise(mistakes)
    ->seven_choice
    *Mira, yo tengo la información de lugares que se pueden reservar en la Universidad como espacios individuales de estudio, además de centros de apoyo al estudio que te entregan espacio y orientación. ¿Te gustaría que te de los teléfonos para que puedas averiguar?
    Yaa, muchas gracias. Voy a escribir hoy mismo #title:Ignacio
    ~raise(correctAnswers)
    ->talk_about_services
    
  === talk_about_services ===   
Igual si quisieras en algún momento abordar el tema con otra profundidad sé que en la Universidad también hay apoyo psicológico #title:Tú
    ¿Hay? Mmm No lo sé la verdad, si lo necesito #title:Ignacio
    ->eight_choice
    
 === eight_choice ===
¿Qué debería decir? #title:Tú
    *Sí, mira voy a llamar altiro
    ~raise(mistakes)
    ->eight_choice
    *Si, déjame mandar un mail altiro contando lo que te pasó 
    ~raise(mistakes)
    ->eight_choice
    *Sí hay ese tipo de servicio. Si piensas que no lo necesitas aún o quieres darle una vuelta, ¿te parece que te deje la información de contacto para que sepas cómo hacerlo por si más adelante cambias de opinión?
    Gracias, se siente bien no ser juzgado y que me hayas escuchado #title:Ignacio
    ~raise(correctAnswers)
    ->nine_choice
    
=== nine_choice ===
¿Qué debería decir? #title:Tú
    *Me alegra saber que estás más tranquilo. La verdad es que yo te acompañé hoy, pero fuiste tú mismo quien encontró la forma de encontrar calma ¿Qué crees que te sirvió?
    ~raise(correctAnswers)
    Respirar mejor, nunca había pensado que servía. Y ordenar mi cabeza, mis pensamientos ufff eso fue muy importante, gracias #title:Ignacio
    ->ten_choice
    *Que bueno que estás más tranquilo. Igual siento que no me contaste todo 
    ~raise(mistakes)
    ->nine_choice

//**** QUINTO PASO *****//

=== ten_choice ===
¿Qué debería decir? #title:Tú
    *Que bueno que pudiste identificar que te hizo sentir bien. A veces las crisis, como la que tuviste hoy, pueden dejarnos algunos síntomas por algunos días. Entonces si volviera a pasar, recuerda la importancia de respirar, ordenar tus ideas y priorizar lo que te haga mejor
    ~raise(correctAnswers)
    *Que bueno que dijiste lo anterior, porque probablemente te volverás a sentir mal, a veces incluso peor 
    ~raise(mistakes)
    *Recuerda que tienes que ir al psicólogo para que no te vuelva a pasar porque podría ser más fuerte la crisis 
    ~raise(mistakes)
-Gracias lo tendré en consideración#title:Ignacio
->END