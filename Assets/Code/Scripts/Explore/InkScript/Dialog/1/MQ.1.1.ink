EXTERNAL playavatar(avatarvalue)
EXTERNAL nameavatar(namevalue)
EXTERNAL ekspresiavatar(eksvalue)
EXTERNAL complete(compvalue)

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Speak")
#speaker:Sacra
Vana, wake up! it's morning.

~ playavatar("3")
~ nameavatar("Vana")
~ ekspresiavatar("Default")
#speaker:Vana
.......

~ playavatar("1")
~ nameavatar("Sacra")
~ ekspresiavatar("Default")
~ complete("1")
#speaker:Sacra
No answer? I have to check inside.
-> END