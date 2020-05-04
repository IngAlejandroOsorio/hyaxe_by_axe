const fs = require("fs");

const saveDirectory = "CustomCharacters";
const freemodeCharacters = [mp.joaat("mp_m_freemode_01"), mp.joaat("mp_f_freemode_01")];
const creatorPlayerPos = new mp.Vector3(402.8664, -996.4108, -99.00027);
const creatorPlayerHeading = -185.0;

const creaTiendaPos = new mp.Vector3(-705.3339,-152.55324,-99.00027);
const creaTiendaHea = 148.534;

const creaPeluPos = new mp.Vector3(-813.9624,-182.41513,37.568905);
const creaPeluHea = 26.85495;

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

mp.events.add("playerLoadCharacter", (player) => {
    player.loadCharacter();
});

mp.events.add("SetRopaPj", (player, slot, drawable, texture) => {
    
    slot = Number(slot);
    drawable = Number(drawable);
    texture = Number(texture);

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
                    if (torsoDataFemale[drawable][texture].BestTorsoDrawable != -1) player.setClothes(3, torsoDataFemale[drawable][texture].BestTorsoDrawable, torsoDataFemale[drawable][texture].BestTorsoTexture, 2);
                }

            }
            
        }else{

            player.setClothes(slot, drawable, texture, 2)

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
    player.sendToWorld();
    if(!player.getVariable("CREATOR_MODE")) player.call("FinishAlphaCreationPj");
});

mp.events.add("creator_Leave", (player) => {
    if (player.changedGender) player.loadCharacter(); // revert back to last save if gender is changed
    player.applyCharacter();
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


mp.events.addCommand("pos", (player,mod,ctd) => {
var playerPos = player.position;
var neo;
if (mod == 1) {
    neo = ctd;
}else{
    neo = playerPos.z + ctd;
}

player.position = new mp.Vector3(playerPos.x, playerPos.y, neo);
});
