const localPlayer = mp.players.local;
let activateNitro = false;
let vehiclesWithNitro = [];
let exhausts = ["exhaust", "exhaust_2", "exhaust_4", "exhaust_5", "exhaust_6", "exhaust_7"];

mp.game.streaming.requestNamedPtfxAsset("core");

mp.events.add({
    'toggleNitroEffect': (state, v) => {
        if (state) {
            if (v && v.handle !== 0) vehiclesWithNitro.push(v);
        } else {
            let indx = vehiclesWithNitro.indexOf(v);
            if (indx != -1) {
                vehiclesWithNitro.splice(indx, 1);
            }
        }
    },

    'giveNitro': (amount, infinite) => {
        mp.game.invoke("0xF2CC03703BDF51A4", localPlayer.vehicle.handle, amount, infinite ? 1 : 0);
    }
});

mp.events.add('render', () => {
    if (mp.game.controls.isControlPressed(0, 73) && localPlayer.vehicle) {
        if (!activateNitro && localPlayer.vehicle.nosAmount > -1.1) {
                if (localPlayer.vehicle.nosAmount != -1){
                toggleNitro(true);
                let tiempo = localPlayer.vehicle.nosAmount + 1;
                localPlayer.vehicle.setForwardSpeed(localPlayer.vehicle.getSpeed() + 20);
                setTimeout(() => {
                localPlayer.vehicle.setForwardSpeed(localPlayer.vehicle.getSpeed() - 15);
                localPlayer.vehicle.nosAmount = -1;      
             }, tiempo * 1000);
            }            
        }
    }

    if (localPlayer.vehicle && localPlayer.vehicle.nosAmount < -0.9) {
        toggleNitro(false);
    }
    
    if (vehiclesWithNitro.length > 0) {
        vehiclesWithNitro.forEach((v) => {
            try {
                if (mp.game.streaming.hasNamedPtfxAssetLoaded("core")) {
                    let heading = v.getHeading();
                    let pitch = v.getPitch();
                    exhausts.forEach((element) => {
                        let boneIndex = mp.game.invoke('0xFB71170B7E76ACBA', v.handle, element); // GET_ENTITY_BONE_INDEX_BY_NAME
                        if (boneIndex >= 0) {
                            let boneCoords = v.getWorldPositionOfBone(boneIndex);
                            mp.game.graphics.setPtfxAssetNextCall("core");
                            if (mp.game.controls.isControlPressed(0, 71)) {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                0.0, pitch, heading - 88, 1, false, false, false);
                            } else {
                                mp.game.graphics.startParticleFxNonLoopedAtCoord("veh_backfire", boneCoords.x, boneCoords.y, boneCoords.z,
                                0.0, pitch, heading - 88, 0.4, false, false, false);
                            }
                           
                        }
                    });
                } else {
                    mp.game.streaming.requestNamedPtfxAsset("core");
                }
            } catch (e) {
                mp.gui.chat.push(e.toString());
            }
        });
    }
});

function toggleNitro(state) {
    if (state) {
        activateNitro = true;
        mp.events.callRemote("NITRO_START");
    } else {
        activateNitro = false;
        mp.events.callRemote("NITRO_STOP");
    }
};