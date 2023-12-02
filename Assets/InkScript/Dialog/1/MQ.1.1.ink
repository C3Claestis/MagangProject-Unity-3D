EXTERNAL playavatar(avatarvalue)
EXTERNAL nameavatar(namevalue)
EXTERNAL ekspresiavatar(eksvalue)
EXTERNAL complete(compvalue)

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Speak")
#speaker:Sacra #job:Sang kakak
Vana, wake up! it's morning.

~ playavatar("3")
~ nameavatar("Vana")
~ ekspresiavatar("Default")
#speaker:Vana #job:Sang Adik
.......

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Default")
#speaker:Sacra #job:Sang Kakak
No answer? I have to check inside.
~ complete("1")
-> END