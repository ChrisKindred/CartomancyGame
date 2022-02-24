== Default ==
Hello test
question?
->My_Choices

==My_Choices==
+ [answer1] 
-> Response1
+ [answer2] 
-> Response2
+ [answer3] 
-> Response3

== Response1 ==
Response 1
* go back to start 
-> Repeat
* test1 
-> Stop

== Response2 ==
Response 2
* go back to start 
-> Repeat
* test2 
-> Stop

== Response3 ==
Response 3
* go back to start 
-> Repeat
* test3 
-> Stop

== Stop ==
This is the end of the test.
-> END

== Repeat ==
let's go back to the test
->My_Choices
