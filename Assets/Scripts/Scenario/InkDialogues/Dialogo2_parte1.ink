VAR correctAnswers = 0

VAR scoreSectionA = 0
VAR scoreSectionB = 0
VAR scoreSectionC = 0
VAR scoreSectionD = 0
VAR scoreSectionE = 0

VAR mistakes = 0
 
 
 ->start
 
 /*--------------------------------------------------------------------------------
	Wrap up character movement using functions, in case we want to develop this logic in future
--------------------------------------------------------------------------------*/

//los consejos no se si ponerlos en comillas dobles.

=== function lower(ref x)
 	~ x = x - 1

=== function raise(ref x)
 	~ x = x + 1

=== function mistake_dialogue()
    (Espera, eso no tiene sentido)


=== start ===
Estás en el trabajo haciendo las tareas correspondientes de tu día a día, de pronto tu compañero Rodrigo te habla porque al parecer Jorge está pasando por una crisis de pánico. Rodrigo no se siente preparado para este tipo de situaciones, pero sabe que usaste EmpathySense y que tú si tienes el conocimiento necesario.  #title:Contexto #dialog:normal
Oye, algo le pasa a Jorge, parece que está llorando.  #title:Rodrigo 
Se nota que está mal, quizá le pasó algo o anda estresado, igual es entendible, le ha tocado harta pega estos días.
Podrías ir a ver qué le pasa, tú sabes cómo ayudar, estudiaste qué hacer en estos casos.
Le dije que podía esperar un rato en la sala de reuniones, pero es todo lo que se me ocurrió decir.
->first_choice


=== first_choice ===
¿Qué debería responder? #title:Tú
    * Voy, pero no prometo nada.
        ~ mistake_dialogue()
        //~ raise(mistakes)
        ->first_choice
        
    * Iré a verlo, para acompañarlo #dialog:guide
        //~ raise(correctAnswers)
        
    * Deberías ir tú a verlo, cualquier puede hacerlo, aunque no estés preparado/a
        ~ mistake_dialogue()
        //~ raise(mistakes)
        ->first_choice
    
- La sala de reuniones está a la izquierda #title:Contexto #dialog:normal
->END
