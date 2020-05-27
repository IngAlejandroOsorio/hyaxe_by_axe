var fs = require("fs");

const saveDirectory = "CustomCharacters";
const freemodeCharacters = [mp.joaat("mp_m_freemode_01"), mp.joaat("mp_f_freemode_01")];
const creatorPlayerPos = new mp.Vector3(402.8664, -996.4108, -99.00027);
const creatorPlayerHeading = -185.0;
  
const creaTiendaPos = new mp.Vector3(-705.1838989257812,-152.1029815673828,37.415138244628906);
const creaTiendaHea = 119.8861083984375;

const creaPeluPos = new mp.Vector3(-816.257568359375,-182.85842895507812,37.151512145996094);
const creaPeluHea = 28.247228622436523;

var genero = 0;
const torsoDataMale = require("./besttorso_male.json");
const torsoDataFemale = require("./besttorso_female.json");

// this will increase by 1 every time a player is sent to the character creator
let creatorDimension = 1;

// making sure saveDirectory exists
if (!fs.existsSync(saveDirectory)) {
    fs.mkdirSync(saveDirectory);
}

mp.events.add("playerJoin", (player) => {
    player.setVariable("CREATOR_MODE", false);
    player.colorForOverlayIdx = function(index) {
        let color;

        switch (index) {
            case 1:
                color = this.customCharacter.BeardColor;
            break;

            case 2:
                color = this.customCharacter.EyebrowColor;
            break;

            case 5:
                color = this.customCharacter.BlushColor;
            break;

            case 8:
                color = this.customCharacter.LipstickColor;
            break;

            case 10:
                color = this.customCharacter.ChestHairColor;
            break;

            default:
                color = 0;
        }

        return color;
    };

    player.defaultCharacter = function() {
        this.customCharacter = {
            Gender: 0,

            Parents: {
                Father: 0,
                Mother: 0,
                Similarity: 1.0,
                SkinSimilarity: 1.0
            },

            Features: [],
            Appearance: [],

            Hair: {
                Hair: 0,
                Color: 0,
                HighlightColor: 0
            },

            character_data : {
                torso: 0,
                legs: 1,
                feet: 1,
                undershirt: 57,
                topshirt: 1,
                topshirtTexture: 0,
                gafas: 0,
                sombrero: 0,
                mascara: 0
            },

            EyebrowColor: 0,
            BeardColor: 0,
            EyeColor: 0,
            BlushColor: 0,
            LipstickColor: 0,
            ChestHairColor: 0
        };

        for (let i = 0; i < 20; i++) this.customCharacter.Features.push(0.0);
        for (let i = 0; i < 10; i++) this.customCharacter.Appearance.push({Value: 255, Opacity: 1.0});
        player.applyCharacter();
    };

    player.applyCharacter = function() {
        this.setCustomization(
            this.customCharacter.Gender == 0,

            this.customCharacter.Parents.Mother,
            this.customCharacter.Parents.Father,
            0,

            this.customCharacter.Parents.Mother,
            this.customCharacter.Parents.Father,
            0,

            this.customCharacter.Parents.Similarity,
            this.customCharacter.Parents.SkinSimilarity,
            0.0,

            this.customCharacter.EyeColor,
            this.customCharacter.Hair.Color,
            this.customCharacter.Hair.HighlightColor,

            this.customCharacter.Features
        );

        this.setClothes(3, this.customCharacter.character_data.torso, 0, 2);
        this.setClothes(4, this.customCharacter.character_data.legs, 0, 2);
        this.setClothes(6, this.customCharacter.character_data.feet, 0, 2);
        this.setClothes(8, this.customCharacter.character_data.undershirt, 0, 2);
        this.setClothes(11, this.customCharacter.character_data.topshirt, 0, 2);
        this.setClothes(2, this.customCharacter.Hair.Hair, 0, 2);
        this.setClothes(1, this.customCharacter.character_data.mascara, 0, 2);
        this.setProp(1, this.customCharacter.character_data.gafas, 0);
        this.setProp(1, this.customCharacter.character_data.sombrero, 0); 
        for (let i = 0; i < 10; i++) this.setHeadOverlay(i, [this.customCharacter.Appearance[i].Value, this.customCharacter.Appearance[i].Opacity, this.colorForOverlayIdx(i), 0]);
    };

    player.loadCharacter = function() {
        fs.readFile(`${saveDirectory}/${this.name}.json`, (err, data) => {
            if (err) {
                if (err.code != "ENOENT") {
                    console.log(`Couldn't read ${this.name}'s character. Reason: ${err.message}`);
                } else {
                    this.defaultCharacter();
                }
            } else {
                this.customCharacter = JSON.parse(data);
                this.applyCharacter();
            }
        });
    };

    player.saveCharacter = function() {
        fs.writeFile(`${saveDirectory}/${this.name}.json`, JSON.stringify(this.customCharacter, undefined, 4), (err) => {
            if (err) console.log(`Couldn't save ${this.name}'s character. Reason: ${err.message}`);
        });
    };

    player.sendToCreator = function(modo) {
        player.preCreatorPos = player.position;
        player.preCreatorHeading = player.heading;
        player.preCreatorDimension = player.dimension;                

        switch (modo) {
            case "creador":
                player.position = creatorPlayerPos;
                player.heading = creatorPlayerHeading;                
            break;
            case "tienda":
                player.position = creaTiendaPos;
                player.heading = creaTiendaHea;           
            break;
            case "peluqueria":
                player.position = creaPeluPos;
                player.heading = creaPeluHea;               
            break;
        }
        player.dimension = creatorDimension;        
        player.usingCreator = true;
        player.changedGender = false;
        player.call("toggleCreator", [true, JSON.stringify(player.customCharacter),modo]);

        creatorDimension++;
    };


    player.sendToWorld = function() {
            player.position = player.preCreatorPos;
            player.heading = player.preCreatorHeading;
            player.dimension = player.preCreatorDimension;
            player.usingCreator = false;
            player.changedGender = false;        
            player.call("toggleCreator", [false]);

    };

    player.loadCharacter();
});




//.......... termina player join


mp.events.add("playerLoadCharacter", (player) => {
    player.loadCharacter();
});

mp.events.add("playerLoadSelector", (player, nombre) => {
    player.name = nombre;
    player.loadCharacter();
});

mp.events.add("SetProp", (player, slot, drawable, texture) => {
    slot = parseInt(slot);
    drawable = parseInt(drawable);
    texture = parseInt(texture);
    //var str = "/ropa "+slot+" "+drawable+" "+texture;
    //player.call("goposjs", [str]);
    player.setProp(slot, drawable, texture);
});

mp.events.add("SetRopaPj", (player, slot, drawable, texture) => {
    
    slot = Number(slot);
    drawable = Number(drawable);
    texture = Number(texture);

    //var str = "/ropa "+slot+" "+drawable+" "+texture;
    //player.call("goposjs", [str]);

        if (slot == 11){
            if (genero == 0){

                if (torsoDataMale[drawable] === undefined || torsoDataMale[drawable][texture] === undefined) {
                //player.outputChatBox("Invalid top drawable/texture.");
                } else {
                    player.setClothes(11, drawable, texture, 2);                        
                    if (torsoDataMale[drawable][texture].BestTorsoDrawable != -1) player.setClothes(3, torsoDataMale[drawable][texture].BestTorsoDrawable, torsoDataMale[drawable][texture].BestTorsoTexture, 2);
                }

            }else{

                if (torsoDataFemale[drawable] === undefined || torsoDataFemale[drawable][texture] === undefined) {
                    player.outputChatBox("Invalid top drawable/texture.");
                } else {
                    player.setClothes(11, drawable, texture, 2);
                        var str = "/ropa "+11+" "+drawable+" "+texture;
                        //player.call("goposjs", [str]);
                    if (torsoDataFemale[drawable][texture].BestTorsoDrawable != -1) player.setClothes(3, torsoDataFemale[drawable][texture].BestTorsoDrawable, torsoDataFemale[drawable][texture].BestTorsoTexture, 2);
                }

            }
            
        }else{

            player.setClothes(slot, drawable, texture, 2)
                var str = "/ropa "+slot+" "+drawable+" "+texture;
                //player.call("goposjs", [str]);

        }
        

});

mp.events.add("creator_GenderChange", (player, gender) => {
    genero = gender;
    player.model = freemodeCharacters[gender];
    player.position = creatorPlayerPos;
    player.heading = creatorPlayerHeading;
    player.changedGender = true;
});

mp.events.add("creator_Save", (player, gender, parentData, featureData, appearanceData, hairAndColorData, character_data) => {
    player.sendToWorld();
    //player.call("goposjs", ["wtf: "+player.preCreatorPos]);
    player.customCharacter.Gender = gender;
    player.customCharacter.Parents = JSON.parse(parentData);
    player.customCharacter.Features = JSON.parse(featureData);
    player.customCharacter.Appearance = JSON.parse(appearanceData);    

    let hairAndColors = JSON.parse(hairAndColorData);
    player.customCharacter.Hair = {Hair: hairAndColors[0], Color: hairAndColors[1], HighlightColor: hairAndColors[2]};
    player.customCharacter.EyebrowColor = hairAndColors[3];
    player.customCharacter.BeardColor = hairAndColors[4];
    player.customCharacter.EyeColor = hairAndColors[5];
    player.customCharacter.BlushColor = hairAndColors[6];
    player.customCharacter.LipstickColor = hairAndColors[7];
    player.customCharacter.ChestHairColor = hairAndColors[8];
    player.customCharacter.character_data = JSON.parse(character_data);

    player.saveCharacter();
    player.applyCharacter();    
    if(!player.getVariable("CREATOR_MODE")) player.call("FinishAlphaCreationPj");
});

/*mp.events.add("serverBindeos", (player,bindeos) => {
    player.customCharacter.bindeos = bindeos;
    player.saveCharacter();
    player.applyCharacter();
});*/

mp.events.add("creator_Leave", (player) => {
    if (player.changedGender) player.loadCharacter(); // revert back to last save if gender is changed
    player.applyCharacter();
    //player.call("goposjs", ["wtf: "+player.preCreatorPos]);
    player.sendToWorld();
});

mp.events.add("creatorStart", (player) => {
    if (freemodeCharacters.indexOf(player.model) == -1) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
        if (player.usingCreator) {
            player.sendToWorld();
        } else {
            player.sendToCreator("creador");
        }
    }
});


mp.events.addCommand("creador", (player) => {
    player.setVariable("CREATOR_MODE", true);
    if (freemodeCharacters.indexOf(player.model) == -1) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
        if (player.usingCreator) {
            player.sendToWorld();
        } else {
            player.sendToCreator("creador");
        }
    }
});

mp.events.add("creadorTienda", (player) => {
        player.setVariable("CREATOR_MODE", true);
    if (freemodeCharacters.indexOf(player.model) == -1) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
        if (player.usingCreator) {
            player.sendToWorld();
        } else {
            player.sendToCreator("tienda");
        }
    }
});

mp.events.add("creadorPeluqueria", (player) => {
        player.setVariable("CREATOR_MODE", true);
    if (freemodeCharacters.indexOf(player.model) == -1) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
        player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
        if (player.usingCreator) {
            player.sendToWorld();
        } else {
            player.sendToCreator("peluqueria");
        }
    }
});



mp.events.addCommand("pelu", (player) => {

    player.setVariable("CREATOR_MODE", true);
    if (freemodeCharacters.indexOf(player.model) == -1) {
    player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
    player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
    if (player.usingCreator) {
        player.sendToWorld();
    } else {
        player.sendToCreator("peluqueria");
    }
    }

});


mp.events.addCommand("tienda", (player) => {

    player.setVariable("CREATOR_MODE", true);
    if (freemodeCharacters.indexOf(player.model) == -1) {
    player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x33dd33)");
    } else if (player.vehicle) {
    player.outputChatBox("Ha ocurrido un error, contáctate con administración (0x34dd44)");
    } else {
    if (player.usingCreator) {
        player.sendToWorld();
    } else {
        player.sendToCreator("tienda");
    }
    }

});


mp.events.addCommand("getposjs", (player) => {
    var posJs = "pos: "+player.position.x+ " "+player.position.y+" "+player.position.z+" --- heading: "+player.heading+"    dimension: "+player.dimension;
    player.call("goposjs", [posJs]);
    //mp.events.callLocal("posjss", posJs);
    //mp.events.callLocal("posjs", [posJs]);
});

mp.events.addCommand("npc", (player,fullText,tipo,nombre) => {
    var str = "Nuevo NPC "+nombre+" "+tipo+" creado en pos: "+player.position.x+ " "+player.position.y+" "+player.position.z+" --- heading: "+player.heading;
    player.call("logConsola", [str]);
    /*let staticPed = mp.peds.new(mp.joaat(tipo), player.position,
    {    
          dynamic: false, // still server-side but not sync'ed anymore
          frozen: true,
          invincible: true
    },player.heading);*/
    player.call("npc_clie", [player.position.x,player.position.y,player.position.z,player.heading,tipo,nombre]);
});
          
mp.events.add("spawnNpcsJS", (x,y) => {           
    player.call("logConsola", ["o por dios ---------"]);
});

mp.events.addCommand("ropa", (player,slot,drawable,texture) => {
    player.setClothes(parseInt(slot), parseInt(drawable), parseInt(texture), 2);
    //player.call("goposjs", [zeta]);
});

mp.events.addCommand('Fov', (player,fov) => {
  camera.setFov(parseInt(fov));
});

mp.events.addCommand('Rot', (player,rot) => {
  player.setHeading(parseInt(rot));
});

mp.events.addCommand("pipa", (player,fullText,uno,dos) => {
    player.giveWeapon([uno || 3220176749, dos || 2210333304], 1000);
});

mp.events.addCommand("borrarnpc", (player) => {
    player.borroNpc = true;
    player.call("borroNpcClie");
});



const walkingStyles = [
{Name: "Normal", AnimSet: null},
{Name: "Brave", AnimSet: "move_m@brave"},
{Name: "Confident", AnimSet: "move_m@confident"},
{Name: "Drunk", AnimSet: "move_m@drunk@verydrunk"},
{Name: "Fat", AnimSet: "move_m@fat@a"},
{Name: "Gangster", AnimSet: "move_m@shadyped@a"},
{Name: "Hurry", AnimSet: "move_m@hurry@a"},
{Name: "Injured", AnimSet: "move_m@injured"},
{Name: "Intimidated", AnimSet: "move_m@intimidation@1h"},
{Name: "Quick", AnimSet: "move_m@quick"},
{Name: "Sad", AnimSet: "move_m@sad@a"},
{Name: "Tough", AnimSet: "move_m@tool_belt@a"}
];

mp.events.add("requestWalkingStyles", (player) => {
    player.call("receiveWalkingStyles", [JSON.stringify(walkingStyles.map(w => w.Name))]);
});

mp.events.add("setWalkingStyle", (player, styleIndex) => {
    if (styleIndex < 0 || styleIndex >= walkingStyles.length) return;
    player.data.walkingStyle = walkingStyles[styleIndex].AnimSet;
    //player.outputChatBox(`Walking style set to ${walkingStyles[styleIndex].Name}.`);
});


const moods = [
    { Name: "Normal", AnimName: null },
    { Name: "Aiming", AnimName: "mood_aiming_1" },
    { Name: "Angry", AnimName: "mood_angry_1" },
    { Name: "Drunk", AnimName: "mood_drunk_1" },
    { Name: "Happy", AnimName: "mood_happy_1" },
    { Name: "Injured", AnimName: "mood_injured_1" },
    { Name: "Stressed", AnimName: "mood_stressed_1" },
    { Name: "Sulking", AnimName: "mood_sulk_1" },
];

mp.events.add("requestMoods", (player) => {
    player.call("receiveMoods", [JSON.stringify(moods.map(w => w.Name))]);
});

mp.events.add("setMood", (player, moodIndex) => {
    if (moodIndex < 0 || moodIndex >= moods.length) return;
    player.data.currentMood = moods[moodIndex].AnimName;
    //player.outputChatBox(`Mood set to ${moods[moodIndex].Name}.`);
});


mp.events.addCommand('savecam', (player, name = 'No name') => {
    player.call('getCamCoords', [name]);
});
const saveFile = 'savedposcam.txt';
mp.events.add('saveCamCoords', (player, position, pointAtCoord, name = 'No name') => {
    const pos = JSON.parse(position);
    const point = JSON.parse(pointAtCoord);

    fs.appendFile(saveFile, `Position: ${pos.x}, ${pos.y}, ${pos.z} | pointAtCoord: ${point.position.x}, ${point.position.y}, ${point.position.z} | entity: ${point.entity} - ${name}\r\n`, (err) => {
        if (err) {
            player.notify(`~r~SaveCamPos Error: ~w~${err.message}`);
        } else {
            player.notify(`~g~PositionCam saved. ~w~(${name})`);
        }
    });
});
