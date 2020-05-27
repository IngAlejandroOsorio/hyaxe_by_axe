const Offset = require("character/animations/offsets.js")

var anim_menu = null;
var anim_playing = false;
var animArr = []
var bindeos = [];

mp.events.add("CargoBindeosClie", (arr) => { // <------------------------------------------------------------------
    mp.players.local.bindeos = JSON.parse(arr);
    bindeos = arr;
    //mp.events.callRemote("posjss", "Cargo Bindeos ---- "+mp.players.local.bindeos);  
    menuCef.execute(`bindeos(${mp.players.local.bindeos})`);
    //mp.events.callRemote("posjss", "Cargo Bindeos ---- "+mp.players.local.bindeos);    
});

mp.events.add('ReceiveAnims', (arg) => {
    animArr = arg;
    //mp.events.callRemote("posjss", "Cargo Receive anims Bindeos ---- "+mp.players.local.bindeos);  
    menuCef.execute(`bindeos(${mp.players.local.bindeos})`);
    setTimeout(() => {menuCef.execute(`animaciones('${arg}')`)},500);    
});

mp.events.add("MasAnimaciones", (index) => { // <------------------------------------------------------------------ 
    anim_playing = true;
    menuAbierto = false;
    if(subMenuCef) {subMenuCef.destroy()}        
    subMenuCef = mp.browsers.new("package://axe/menu/animaciones.html");
    //mp.events.callRemote("posjss", "anims 21 ---- "+mp.players.local.bindeos);
    subMenuCef.execute(`reciboBindeos(${mp.players.local.bindeos})`);
    setTimeout(() => {subMenuCef.execute(`masAnimaciones('${animArr}')`)},500);
});

mp.events.add("SalirMenuPjMasAnimaciones", (index) => {
if(subMenuCef) {subMenuCef.destroy();subMenuCef = undefined};
    menuAbierto = false;
    setTimeout(() => {mp.gui.cursor.visible = false;mp.gui.cursor.show(false, false)},200);
});

mp.events.add('SetAnimPlaying', (data) => {
    anim_playing = data;
});


mp.events.add('CallAnimList', () => {
    if (anim_menu == null) mp.events.callRemote("RequestAnims");
    else anim_menu.Visible = !anim_menu.Visible;
});

/*
setInterval(() => {
    if (mp.keys.isDown(0x57) === true || // W
        mp.keys.isDown(0x41) === true || // A
        mp.keys.isDown(0x53) === true || // S
        mp.keys.isDown(0x44) === true) { // D
        if (anim_playing) mp.events.callRemote("StopPlayerAnim");
    }
}, 500);
*/