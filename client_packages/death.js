/*
    Camera Effects:

    0 - No/cancel effect
    1 - Effect zooms in, gradually tilts cam clockwise apx 30 degrees, wobbles slowly. Motion blur is active until cancelled
    2 - Effect immediately tilts cam clockwise apx 30 degrees, begins to wobble slowly, then gradually tilts cam back to normal. The wobbling will continue until the effect is cancelled

    Source: NativeDB
*/

function getDateString(){
    var d = new Date();
     return d.toLocaleTimeString();
}

const scriptConstants = {
    screenFX: "DeathFailMPIn",
    textDelay: 750,
    camEffect: 1
};

let textTimer = null;

mp.events.add("playerDeathh", (player, reason, killer) => {
    mp.game.audio.playSoundFrontend(-1, "Bed", "WastedSounds", true);
    mp.game.graphics.startScreenEffect(scriptConstants.screenFX, 0, true);
    mp.game.cam.setCamEffect(scriptConstants.camEffect);

    if (textTimer) clearTimeout(textTimer);
    textTimer = setTimeout(function() {
        mp.game.ui.messages.showShard("~r~Has muerto", getDateString());
    }, scriptConstants.textDelay);
});

mp.events.add("playerSpawn", () => {
    mp.game.graphics.stopScreenEffect(scriptConstants.screenFX);
    mp.game.cam.setCamEffect(0);

    if (textTimer) {
        clearTimeout(textTimer);
        textTimer = null;
    }
});