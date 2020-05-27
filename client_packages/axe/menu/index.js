//AxE Is On Top

var menuCef = mp.browsers.new("package://axe/menu/index.html");
setTimeout(() => {mp.events.callRemote("RequestAnims")},1000);
var subMenuCef;
var menuAbierto = false;
var anim_playing = false;


mp.events.add("AbrirConfig", () => { // <------------------------------------------------------------------
	if(subMenuCef) {subMenuCef.destroy()}
	subMenuCef = mp.browsers.new("package://statics/settings/settings.html");
});


mp.events.add("IniciarAnimacion", (index) => { // <------------------------------------------------------------------
	mp.events.callRemote("PlayAnim", index);
	anim_playing = true;
});

mp.events.add("AbrirInfo", (index) => { // <------------------------------------------------------------------
	if(subMenuCef) {subMenuCef.destroy()}
	subMenuCef = mp.browsers.new("package://statics/settings/info.html");
});

mp.events.add("SalirMenuPj", () => { // <------------------------------------------------------------------
	if(subMenuCef) {subMenuCef.destroy();subMenuCef = undefined};
	menuAbierto = false;
	setTimeout(() => {mp.gui.cursor.visible = false;mp.gui.cursor.show(false, false)},200);
});

mp.events.add("SalirMenuPjSoft", () => { // <------------------------------------------------------------------
	if(subMenuCef) {subMenuCef.destroy();subMenuCef = undefined};
	menuAbierto = false;
	setTimeout(() => {mp.gui.cursor.visible = true;mp.gui.cursor.show(true, true)},200);
});

mp.events.add("guardarBindeos", (arr) => { // <------------------------------------------------------------------
	mp.players.local.bindeos = arr;
	//mp.events.callRemote("posjss", "envio arr al server"+arr);
	mp.events.callRemote("updateBindeos", arr);
	menuCef.execute(`bindeos(${mp.players.local.bindeos})`);
});

mp.events.add("menuCoche", () => {
	setTimeout(() => {mp.gui.cursor.visible = true;mp.gui.cursor.show(false, true)},200);
	mp.events.callRemote("ActionMenuVehicle");
});

mp.keys.bind(0x71, false, () => { // F2 localPlayer.clearTasksImmediately();
	if (menuAbierto){
		menuAbierto = false;
		menuCef.execute(`toggle(false)`);
		setTimeout(() => {mp.gui.cursor.visible = false;mp.gui.cursor.show(false, false)},200);
	}else{
		menuAbierto = true;
		menuCef.execute(`toggle(true)`);
		setTimeout(() => {mp.gui.cursor.visible = true;mp.gui.cursor.show(true, true)},200);
	}
   
});

/*mp.keys.bind(0x72, false, () => { // F3
    menuCef.execute(`animaciones('["~r~Parar anim","¿Donde estoy?","¿Que es eso?","AplaudirF","AplaudirM","Aplaudir sin ganas","Apoyarse en la pared","Apoyarse manos juntas","Apuntar en papel","Arreglar bombilla","Avisar por radio","Brazos cruzadosF","Brazos cruzadosM","Borracho 1","Borracho 2","Borracho 3","Celebrar","Con ritmoM","Con ritmoF","Charlar","Decepcion","Depresion","Estirar musculos","Flexion","Fumar apoyado 1","Fumarapoyado 2","Fumar apoyado 3","Fumar apoyado 4","Fumar apoyado 5","Fumar tranquilo","Fumar tranquila","Grabar mobil","Levantar Mano","Limpiar manos agachado","Me duele la espalda","Mirar el suelo","Nervioso","Posar","SentarseM","SentarseF","Sentarse pies cruzados","Sentarse piernas cruzadas","Sentarse con pie fuera","Sentarse tímido","Sentarse cansado 1","Sentarse cansado 2","Sentarse apoyado","Sentarse con musica","Sentarse a disfrutar 1","Sentarse a disfrutar 2","Sentarse adisfrutar 3","Sentarse a disfrutar 4","Sentarse piernas abiertas","Timida","Tranquilo tranquilo","Vigilar el area"]')`);
});*/


let walkingStyles = null;
let currentItem = 0;

function setWalkingStyle(player, style) {
    if (!style) {
        player.resetMovementClipset(0.0);
    } else {
        if (!mp.game.streaming.hasClipSetLoaded(style)) {
            mp.game.streaming.requestClipSet(style);
            while(!mp.game.streaming.hasClipSetLoaded(style)) mp.game.wait(0);
        }

        player.setMovementClipset(style, 0.0);
    }
}

mp.events.add("SetEstiloCaminar", (index) => {
    mp.events.callRemote("setWalkingStyle", index);
});


// events
mp.events.add("receiveWalkingStyles", (namesJSON) => {
    walkingStyles = JSON.parse(namesJSON);
	mp.players.local.estiloCaminar = walkingStyles;

});

mp.events.add("entityStreamIn", (entity) => {
    if (entity.type !== "player") return;
    setWalkingStyle(entity, entity.getVariable("walkingStyle"));
});

mp.events.addDataHandler("walkingStyle", (entity, value) => {
    if (entity.type === "player") setWalkingStyle(entity, value);
});


let moods = null;
let currentItemj = 0;

function setMood(player, mood) {
    if (!mood) {
        player.clearFacialIdleAnimOverride();
    } else {
        mp.game.invoke("0xFFC24B988B938B38", player.handle, mood, 0);
    }
}


mp.events.add("jetas", (index) => {
    mp.events.callRemote("setMood", index);
    currentItemj = index;
});


mp.events.add("receiveMoods", (namesJSON) => {
    moods = JSON.parse(namesJSON);
});

mp.events.add("entityStreamIn", (entity) => {
    if (entity.type === "player") setMood(entity, entity.getVariable("currentMood"));
});

mp.events.addDataHandler("currentMood", (entity, value) => {
    if (entity.type === "player") setMood(entity, value);
});

mp.events.add("sonido", (url,segundos) => {
	let args = [url,segundos];
	menuCef.execute(`sonido('${url}',${segundos})`);
});