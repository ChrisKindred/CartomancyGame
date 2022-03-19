== Default ==
Hello <i>test</i>
question? 
->My_Choices

==My_Choices== //repeatable choice
#garf #runsLeftTo #left 
Select
+ [answer1] 
-> Response1
+ [answer2] 
-> Response2
+ [answer3] 
-> Response3

== Response1 ==
#garf #runsRightTo #left 
#tri #starts #right 
Response 1
* [go back to start] 
-> My_Choices
* [test1] 
-> Stop

== Response2 ==
Response 2
* [go back to start] 
-> My_Choices
* [test2] 
-> Stop

== Response3 ==
Response 3
* [go back to start] 
-> My_Choices
* [test3] 
-> Stop

== Stop ==
This is the end of the test.
-> END

== Repeat ==
let's go back to the test
->My_Choices
