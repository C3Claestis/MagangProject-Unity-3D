EXTERNAL playavatar(avatarvalue)
EXTERNAL nameavatar(namevalue)
EXTERNAL ekspresiavatar(eksvalue)
EXTERNAL complete(compvalue)

~ playavatar("1")
~ nameavatar("Vana")
~ ekspresiavatar("Smile")
#speaker:NPC #job:Test
Vana apakah kamu sudah bangun?

~ playavatar("1")
~ nameavatar("Vana")
~ ekspresiavatar("Smile")
#speaker:NPC #job:Test
Bangun woi

~ playavatar("1")
~ nameavatar("Vana")
~ ekspresiavatar("Default")
#speaker:NPC #job:Test
Sekarang bangun

~ playavatar("3")
#speaker:Vana #job:Sang Adik
..... (Vana tidak merespon)


~ playavatar("1")
~ nameavatar("Vana")
~ ekspresiavatar("Default")
#speaker:Sacra #job:Sang Kakak
Kemana dia? 

~ playavatar("1")
~ nameavatar("Vana")
~ ekspresiavatar("Speak")
~ complete("2")
Aku masuk ya Vana!
-> END