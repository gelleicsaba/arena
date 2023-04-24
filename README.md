# Arena  

Az arénában N db hősök küzdenek, akik lehetnek íjászok, lovasok és kardosok.  Minden hős rendelkezik egy azonosítóval és életerővel, valamint a lenti szabály szerint tudnak támadni és védekezni.
* Íjász támad 
	* lovast: 40% eséllyel a lovas meghal, 60%-ban kivédi
	* kardost: kardos meghal 
	* íjászt: védekező meghal
*	Kardos támad 
	* lovast: nem történik semmi
	* kardost: védekező meghal 
	* íjászt: íjász meghal
*	Lovas támad
	* lovast: védekező meghal
	* kardost: lovas meghal 
	* íjászt: íjász meghal

A csata körökre van lebontva, minden körbe véletlenszerűen kiválasztásra kerül egy támadó és egy védekező. A kimaradt hősök pihennek és növekszik az élet erejük 10-el, viszont nem mehet a maximum fölé.  

A harcban résztvevő hősök életereje a felére csökken, ha ez kisebb mint a kezdeti életerő negyede akkor meghalnak. Kezdeti életerők íjász: 100 lovas: 150 kardos: 120.

A csata elindítása előtt le kell generálni N darab véletlenszerű hőst, amit paraméterként fog megkapni. Csata addig tart még maximum 1 hős marad életben. Minden kör végén logolni kell ki támadott meg kit és hogyan változott az életerejük. Készíts egy olyan consol applikációt ami a fenti szabályait figyelembe véve hősöket csatáztat egymással. 
