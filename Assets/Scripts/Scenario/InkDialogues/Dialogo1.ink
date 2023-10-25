VAR correctAnswers = 0

VAR scoreSectionA = 0
VAR scoreSectionB = 0
VAR scoreSectionC = 0
VAR scoreSectionD = 0
VAR scoreSectionE = 0

VAR intentoSectionA = 0
VAR intentoSectionB = 0
VAR intentoSectionC = 0
VAR intentoSectionD = 0
VAR intentoSectionE = 0

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
Vienes saliendo de la universidad, vas caminando por la vereda camino a casa cuando notas que en una plaza está uno de tus amigos. #title:Contexto #audio:feedback1_contexto
Está sentado en una banca mirando al piso, con las manos en la cabeza y llorando efusivamente. Tratas de llamar su atención, pero no se da cuenta de que estás ahí.
Decides acercarte a él para saludarlo. #dialog:normal 
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
        Sí, ya te dije, lo intenté absolutamente todo #title:Ignacio #dialog:guide
        Recuerda no buscar dar soluciones rápidas, esto puede cerrar el diálogo y no ser lo que requiere la persona #dialog:normal #audio:feedback1_01
	    ~raise(intentoSectionA)
        ->first_choice
    * Aún te quedan más pruebas, puedes hacerlo 
        Realmente lo dudo, así como voy estoy necesitando un 8 en cada prueba que queda para poder optar a pasar. #title:Ignacio #dialog:guide
        Este comentario puede minimizar el dolor de la persona y generar falsas esperanzas#dialog:normal #audio:feedback1_02
	    ~raise(intentoSectionA)
        ->first_choice
    * Te entiendo, es muy difícil el intentarlo y no poder concentrarse
        ~raise(scoreSectionA)
	    ~raise(intentoSectionA)
        ->talk_about_parents
        
=== talk_about_parents ===
Es que... cada vez que tengo que estudiar... Mi familia pide que haga cosas o me interrumpe en medio de mi estudio... #title:Ignacio
Lo peor es que si les digo que no puedo hacer lo que me piden se enojan conmigo. 
Ni me imagino como irán a reaccionar cuando se enteren que me está yendo mal en la u. 
->second_choice

=== second_choice ===
    * Deberías conversarlo con ellos, de más que te van a entender 
        Dices eso porque no los conoces, ellos jamás me van a escuchar, si intentara hablarles sobre eso creerían que les estoy faltando el respeto o algo #title:Ignacio #dialog:guide
        Recuerda que tu labor no es dar una solución sino acompañar a la persona en encontrar su propia estabilidad. #dialog:normal #audio:feedback1_03
	    ~raise(intentoSectionA)
        ->second_choice
    * Y dentro de lo que me contaste ¿Piensas que ellos no entienden que estás ocupado cuando estudias?
        ~raise(scoreSectionA)
	    ~raise(intentoSectionA)
        ->talk_about_parents_cont
    * Mis papás también son medio pesados, pero consigo manejarme con ellos.
        (En un tono sarcástico) Vaya, que bueno que tus papás sean capaces de darte un poco de espacio para que puedas hacer tus cosas. Tu vida probablemente es mucho mejor que la mía.  #title:Ignacio
        Disculpa, no quería ofenderte. #title:Tú
        (Suspira) Si, si... claro, entiendo #title:Ignacio #dialog:guide
	    Recuerda que lo importante aquí es el relato de la otra persona, es importante no protagonizar. #dialog:normal #audio:feedback1_04
        ~raise(intentoSectionA)
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
        ~raise(scoreSectionA)
	    ~raise(intentoSectionA)
    * Podría ser peor...
        Eso no me ayuda en nada #title:Ignacio
        (Llora fuertemente) #dialog:guide
	    Este mensaje podría relativizar el dolor de la otra persona #dialog:normal #audio:feedback1_05
        ~raise(intentoSectionA)
	->third_choice
    * Tranquilo, vas a estar bien. #dialog:guide
	    Este comentario puede minimizar el dolor de la persona y generar falsas esperanzas #dialog:normal #audio:feedback1_06
	    ~raise(intentoSectionA)
	->third_choice
-Comienzas a notar que el Ignacio empieza a respirar más rápido mientras le siguen cayendo lágrimas. Con las manos temblorosas intenta fuertemente secarse la cara. #title:Contexto
->help_breathing

//**** SEGUNDO PASO *****//

=== help_breathing ===
¿Qué debería responder? #title:Tú
    * ¿Quieres que llame a alguien? #dialog:guide
	    Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona. #dialog:normal #audio:feedback1_07
        ~raise(intentoSectionB)
	->help_breathing
    * No llores, estoy seguro de que puedes hablar con tus papás. #dialog:guide
	    Este comentario puede minimizar el dolor de la persona y recuerda que tu labor no es dar una solución sino acompañar a la persona en encontrar su propia estabilidad. #dialog:normal #audio:feedback1_08
        ~raise(intentoSectionB)
	->help_breathing
    * ¿Te gustaría que hiciéramos algo que te pueda entregar un poco de tranquilidad ahora?
        ~raise(scoreSectionB)
	~raise(intentoSectionB)
-\(No responde) #title:Ignacio
Mira, vi este video en youtube que decía que regular la respiración ayudaba a las personas a calmarse. #title:Tú
En el video explican que al respirar mejor y más profundo nuestras emociones se pueden regular. Puedo enseñártelo si quieres. 
(Asiente con la cabeza) #title:Ignacio
->breathing_1

=== breathing_1 ===
¿Qué debería decir? #title:Tú
    * Ok, primero nos vamos a sentar en una posición cómoda.
        ~raise(scoreSectionB)
	~raise(intentoSectionB)
    * Ok, pero primero debes dejar de llorar.
        (Continúa llorando)  #title:Ignacio #dialog:guide
	Este comentario puede minimizar el dolor de la otra persona y también recuerda no entrar en un rol directivo.#dialog:normal#audio:feedback1_09
        ~raise(intentoSectionB)
        ->breathing_1
    * Ok, primero debes pensar en algo feliz
        (Te mira con una cara de confusión y de rechazo) #title:Ignacio#dialog:guide
	Recuerda no invisibilizar el dolor de la otra persona ni intentar limitarlo por tu propio interés de concluir pronto.#dialog:normal #audio:feedback1_10
        ~raise(intentoSectionB)
	->breathing_1
-\(Se acomoda en la banca, sentándose en una posición recta)
->breathing_2

=== breathing_2 ===
¿Qué debería decir? #title:Tú
    * Te lo voy a explicar en general primero, lo que haremos será respirar en 4 tiempos, luego botar el aire en 4 tiempos y luego esperar 7 tiempos para volver a respirar#title:Tú
        ~raise(scoreSectionB)
	~raise(intentoSectionB)
    * Cópiame como respiro#title:Tú
        (Espera, eso no tiene sentido)#dialog:guide
	Recuerda la importancia de anticipar a la persona sobre lo que realizarán para que pueda recuperar la sensación de certeza#dialog:normal #audio:feedback1_11
        ~raise(intentoSectionB)
        ->breathing_2
    *Sólo respira como si quisieras inflar tu estómago#title:Tú
        (Espera, eso no tiene sentido)#dialog:guide
	Recuerda que en momentos de crisis es importante entregar mensajes claros que faciliten los pasos siguientes#dialog:normal #audio:feedback1_12
        ~raise(intentoSectionB)
        ->breathing_2
-\(Asiente con la cabeza) #title:Ignacio
->breathing_3

=== breathing_3 ===

¿Qué debería decir? #title:Tú
    *Ok, hagamos la primera parte. Tienes que inhalar y contar hasta 4, a tu ritmo#title:Tú
        ~raise(scoreSectionB)
	~raise(intentoSectionB)    
    *Ok, inhala hasta que yo te avise#title:Tú
	(Respira agitadamente) #title:Ignacio#dialog:guide
	Recuerda no entrar en un rol directivo y que es importante que la persona maneje sus propios tiempos en la respiración para no correr riesgo de hiperventilación#dialog:normal #audio:feedback1_13
        ~raise(intentoSectionB)
	->breathing_3
-\(Respira) #title:Ignacio
->breathing_4

=== breathing_4 ===

¿Qué debería decir? #title:Tú
    *Ahora, exhala contando hasta 4, igualmente a tu ritmo#title:Tú
        ~raise(scoreSectionB)
	~raise(intentoSectionB)    
    *Después, exhala hasta que yo te avise#title:Tú
	(Respira agitadamente) #title:Ignacio#dialog:guide
	Recuerda no entrar en un rol directivo y que es importante que la persona maneje sus propios tiempos en la respiración para no correr riesgo de hiperventilación#dialog:normal  #audio:feedback1_14
        ~raise(intentoSectionB)
	->breathing_4
-\(Respira) #title:Ignacio
->breathing_5

=== breathing_5 ===

¿Qué debería decir? #title:Tú
    *Ahora, cuenta 4 tiempos antes de volver a inhalar
        ~raise(scoreSectionB)
	~raise(intentoSectionB)    
    *Ahora aguante 4 minutos
	(Respira agitadamente) #title:Ignacio#dialog:guide
	Recuerda que es importante que la persona maneje sus tiempos a su propio ritmo#dialog:normal #audio:feedback1_15
        ~raise(intentoSectionB)
	->breathing_5
    *Ahora vuelve a inhalar
	(Respira agitadamente) #title:Ignacio#dialog:guide
	Recuerda no entrar en un rol directivo y que es importante que la persona maneje sus propios tiempos en la respiración#dialog:normal #audio:feedback1_16
        ~raise(intentoSectionB)
	->breathing_5
-\(Respira) #title:Ignacio
->breathing_6

=== breathing_6 ===

-Ahora repite#title:Tú 
-\(Repite las indicaciones que le diste) #title:Ignacio
\(Imita los pasos de respiración junto al afectado)#title:Tú
Bien hecho, sigue así#title:Tú
->after_breathing

=== after_breathing ===

Después de unos minutos haciendo la técnica de respiración, el AFECTADO ha conseguido respirar con calma. 
¿Te sientes mejor? #title:Tú
Creo que si, o sea no bien completo, pero puedo hablar mejor. Gracias #title:Ignacio
->fourth_choice

//**** TERCER PASO *****//

=== fourth_choice ===
¿Qué debería decir? #title:Tú
    *¿Seguro?
    	La verdad es que no, sigo un poco triste. #title:Ignacio#dialog:guide
	Si has logrado que la persona se pueda calmar es importante promover el mantener ese estado. Esta pregunta podría volver la agitación#dialog:normal #audio:feedback1_17
	~raise(intentoSectionC)
	->fourth_choice
    *No deberías alterarte así de rápido, deberías buscar soluciones primero #title:Tú
    	Disculpa, no quise molestarte, pero realmente estoy muy perdido #title:Ignacio
    	No te preocupes, está bien, le puede pasar a cualquiera. #title:Tú#dialog:guide
	Recuerda no realizar comentarios juiciosos ni dar consejos no solicitados#dialog:normal #audio:feedback1_18
    	~raise(intentoSectionC)
    	->fourth_choice
    *¿Crees que podamos retomar un poco de lo que me estabas diciendo antes?
    	Si... Ahora estoy un poco más tranquilo.  #title:Ignacio
    	~raise(scoreSectionC)
	~raise(intentoSectionC)
    ->five_choice
    
=== five_choice ===
¿Qué debería decir? #title:Tú
    *De lo que me contaste antes ¿Qué sientes que es lo más problemático para ti hoy? 
    	Bueno... si tuviera que decir yo... creo que más que nada necesito un lugar donde poder estudiar. #title:Ignacio
    	~raise(scoreSectionC)
	~raise(intentoSectionC)
    	->sixth_choice
    *Realmente creo que debes hablar con tus papás al respecto.
    	Sería una pérdida de tiempo#title:Ignacio#dialog:guide
	Recuerda no dar consejos que no han sido solicitados#dialog:normal #audio:feedback1_19
    	~raise(intentoSectionC)
    	->five_choice
=== sixth_choice ===
¿Qué debería decir? #title:Tú
    *Entonces piensas que tú prioridad en este momento es cubrir la necesidad de un lugar para estudiar tranquilo
    	Si, creo que sí. Pero no sé qué hacer respecto a eso. #title:Ignacio
    	Además está el problema de mis papás, no creo que pueda convencerlos de nada. #title:Ignacio
    	~raise(scoreSectionC)
	~raise(intentoSectionC)
    ->talk_about_study
    *No es necesario que hables con ellos solo, puedes pedirle a tu hermana que te acompañe a conversar
    	No había considerado eso, ella también tiene muchos problemas con ellos. #title:Ignacio#dialog:guide
	Recuerda no dar consejos que no han sido solicitados#dialog:normal #audio:feedback1_21
    	~raise(intentoSectionC)
    	->sixth_choice
    
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
    	No lo sé... no quiero molestarte. #title:Ignacio#dialog:guide
	Recuerda que tu labor no es dar una solución sino acompañar a la persona en encontrar su propia estabilidad#dialog:normal #audio:feedback1_22
    	~raise(intentoSectionD)
    	->seven_choice
    *Voy a llamar a la Universidad para que te ayuden#title:Tú
    	No, por favor no! Necesito ver qué haré antes #title:Ignacio#dialog:guide
	Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona#dialog:normal #audio:feedback1_23
    	~raise(intentoSectionD)
    	->seven_choice
    *Mira, yo tengo la información de lugares que se pueden reservar en la Universidad como espacios individuales de estudio, además de centros de apoyo al estudio que te entregan espacio y orientación. ¿Te gustaría que te de los teléfonos para que puedas averiguar?#title:Tú
    	Yaa, muchas gracias. Voy a escribir hoy mismo #title:Ignacio
    	~raise(scoreSectionD)
	~raise(intentoSectionD)
    	->talk_about_services
    
  === talk_about_services ===   
Igual si quisieras en algún momento abordar el tema con otra profundidad sé que en la Universidad también hay apoyo psicológico #title:Tú
    ¿Hay? Mmm No lo sé la verdad, si lo necesito #title:Ignacio
    ->eight_choice
    
 === eight_choice ===
¿Qué debería decir? #title:Tú
    *Sí, mira voy a llamar altiro#title:Tú
	Nooo, por favor no#title:Ignacio#dialog:guide
	Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona#dialog:normal #audio:feedback1_24
    	~raise(intentoSectionD)
    	->eight_choice
    *Si, déjame mandar un mail altiro contando lo que te pasó#title:Tú
	Nooo, por favor no#title:Ignacio#dialog:guide
	Recuerda que tu labor no es dar una solución sino acompañar a la persona, tomar un rol directivo también puede invalidar a la otra persona#dialog:normal #audio:feedback1_25
    	~raise(intentoSectionD)
    	->eight_choice
    *Sí hay ese tipo de servicio. Si piensas que no lo necesitas aún o quieres darle una vuelta, ¿te parece que te deje la información de contacto para que sepas cómo hacerlo por si más adelante cambias de opinión?#title:Tú
    	Gracias, se siente bien no ser juzgado y que me hayas escuchado #title:Ignacio
    	~raise(scoreSectionD)
	~raise(intentoSectionD)
    	->nine_choice
    
=== nine_choice ===
¿Qué debería decir? #title:Tú
    *Me alegra saber que estás más tranquilo. La verdad es que yo te acompañé hoy, pero fuiste tú mismo quien encontró la forma de encontrar calma ¿Qué crees que te sirvió?
    	~raise(scoreSectionD)
	~raise(intentoSectionD)
    	Respirar mejor, nunca había pensado que servía. Y ordenar mi cabeza, mis pensamientos ufff eso fue muy importante, gracias #title:Ignacio
    	->ten_choice
    *Que bueno que estás más tranquilo. Igual siento que no me contaste todo#dialog:guide
	Si has logrado que la persona se pueda calmar es importante promover el mantener ese estado. Esta pregunta podría volver la agitación, además de generar angustia por tener que re abrir la temática#dialog:normal #audio:feedback1_26
    	~raise(intentoSectionD)
    	->nine_choice

//**** QUINTO PASO *****//

=== ten_choice ===
¿Qué debería decir? #title:Tú
    *Que bueno que pudiste identificar que te hizo sentir bien. A veces las crisis, como la que tuviste hoy, pueden dejarnos algunos síntomas por algunos días. Entonces si volviera a pasar, recuerda la importancia de respirar, ordenar tus ideas y priorizar lo que te haga mejor
    	~raise(scoreSectionE)
	~raise(intentoSectionE)
    *Que bueno que dijiste lo anterior, porque probablemente te volverás a sentir mal, a veces incluso peor#dialog:guide
	Recuerda no entregar un mensaje que pueda generar angustia hacia el futuro#dialog:normal #audio:feedback1_27
    	~raise(intentoSectionE)
	->ten_choice
    *Recuerda que tienes que ir al psicólogo para que no te vuelva a pasar porque podría ser más fuerte la crisis#dialog:guide
	Recuerda no entregar un mensaje que pueda generar angustia hacia el futuro y no dar consejos no solicitados#dialog:normal #audio:feedback1_28
    	~raise(intentoSectionE)
	->ten_choice
-Gracias lo tendré en consideración#title:Ignacio
#EndDialogue:End #achievement:Sim-01
->END