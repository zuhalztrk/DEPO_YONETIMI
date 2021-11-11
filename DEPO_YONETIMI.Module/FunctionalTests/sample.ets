;#DropDB DEPO_YONETIMIEasyTest

#Application DEPO_YONETIMIWin
#Application DEPO_YONETIMIBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef DEPO_YONETIMIWin

*Action About...
 
*Action OK

#EndIf

#IfDef DEPO_YONETIMIBlazor
*Action Navigation(My Details)
#EndIf
