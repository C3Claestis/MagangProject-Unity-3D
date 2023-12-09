EXTERNAL playavatar(avatarvalue)
EXTERNAL nameavatar(namevalue)
EXTERNAL ekspresiavatar(eksvalue)
EXTERNAL complete(compvalue)

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Speak")
#speaker:Sacra #job:Vana's older brother
Vana, wake up! it's morning.

~ playavatar("3")
~ nameavatar("Vana")
~ ekspresiavatar("Default")
#speaker:Vana #job:Sacra's younger sister
.......

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Default")
~ complete("1")
#speaker:Sacra #job:Vana's older brother
No answer? I have to check inside.
-> END