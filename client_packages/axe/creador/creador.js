// shitcode will be better in the future
const NativeUI = require("nativeui");
const Data = require("charcreator/data");

const Menu = NativeUI.Menu;
const UIMenuItem = NativeUI.UIMenuItem;
const UIMenuListItem = NativeUI.UIMenuListItem;
const UIMenuCheckboxItem = NativeUI.UIMenuCheckboxItem;
const BadgeStyle = NativeUI.BadgeStyle;
const Point = NativeUI.Point;
const ItemsCollection = NativeUI.ItemsCollection;
const Color = NativeUI.Color;


const creatorCoords = {
    camera: new mp.Vector3(402.8664, -997.5515, -98.5),
    cameraLookAt: new mp.Vector3(402.8664, -996.4108, -98.5)
};

const localPlayer = mp.players.local;

var creadorCef;

var character_data = {
  torso: 0,
  legs: 1,
  feet: 1,
  undershirt: 57,
  topshirt: 1,
  topshirtTexture: 0,
};

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function colorForOverlayIdx(index) {
    let color;

    switch (index) {
        case 1:
            color = beardColorItem.Index;
        break;

        case 2:
            color = eyebrowColorItem.Index;
        break;

        case 5:
            color = blushColorItem.Index;
        break;

        case 8:
            color = lipstickColorItem.Index;
        break;

        case 10:
            color = chestHairColorItem.Index;
        break;

        default:
            color = 0;
    }

    return color;
}

function updateParents() {
    localPlayer.setHeadBlendData(
        // shape
        Data.mothers[motherItem.Index],
        Data.fathers[fatherItem.Index],
        0,

        // skin
        Data.mothers[motherItem.Index],
        Data.fathers[fatherItem.Index],
        0,

        // mixes
        similarityItem.Index * 0.01,
        skinSimilarityItem.Index * 0.01,
        0.0,

        false
    );
}

function updateFaceFeature(index) {
    localPlayer.setFaceFeature(index, parseFloat(featureItems[index].SelectedValue));
}

function updateAppearance(index) {
    let overlayID = (appearanceItems[index].Index == 0) ? 255 : appearanceItems[index].Index - 1;
    localPlayer.setHeadOverlay(index, overlayID, appearanceOpacityItems[index].Index * 0.01, colorForOverlayIdx(index), 0);
}

function updateHairAndColors() {
    localPlayer.setComponentVariation(2, Data.hairList[currentGender][hairItem.Index].ID, 0, 2);
    localPlayer.setHairColor(hairColorItem.Index, hairHighlightItem.Index);
    localPlayer.setEyeColor(eyeColorItem.Index);
    localPlayer.setHeadOverlayColor(1, 1, beardColorItem.Index, 0);
    localPlayer.setHeadOverlayColor(2, 1, eyebrowColorItem.Index, 0);
    localPlayer.setHeadOverlayColor(5, 2, blushColorItem.Index, 0);
    localPlayer.setHeadOverlayColor(8, 2, lipstickColorItem.Index, 0);
    localPlayer.setHeadOverlayColor(10, 1, chestHairColorItem.Index, 0);
}

function applyCreatorOutfit() {
    /*if (currentGender == 0) {
        localPlayer.setDefaultComponentVariation();
        localPlayer.setComponentVariation(3, 15, 0, 2);
        localPlayer.setComponentVariation(4, 21, 0, 2);
        localPlayer.setComponentVariation(6, 34, 0, 2);
        localPlayer.setComponentVariation(8, 15, 0, 2);
        localPlayer.setComponentVariation(11, 15, 0, 2);
    } else {
        localPlayer.setDefaultComponentVariation();
        localPlayer.setComponentVariation(3, 15, 0, 2);
        localPlayer.setComponentVariation(4, 10, 0, 2);
        localPlayer.setComponentVariation(6, 35, 0, 2);
        localPlayer.setComponentVariation(8, 15, 0, 2);
        localPlayer.setComponentVariation(11, 15, 0, 2);
    }*/
}

function fillHairMenu() {
    hairItem = new UIMenuListItem("Pelo", "Pelo de tu personaje.", new ItemsCollection(Data.hairList[currentGender].map(h => h.Name)));
    creatorHairMenu.AddItem(hairItem);

    hairColorItem = new UIMenuListItem("Color de Pelo", "Color de pelo de tu personaje.", new ItemsCollection(hairColors));
    creatorHairMenu.AddItem(hairColorItem);

    hairHighlightItem = new UIMenuListItem("Color resaltado del pelo", "Color resaltado del pelo de tu personaje.", new ItemsCollection(hairColors));
    creatorHairMenu.AddItem(hairHighlightItem);

    eyebrowColorItem = new UIMenuListItem("Color de Cejas", "El color de cejas de tu personaje", new ItemsCollection(hairColors));
    creatorHairMenu.AddItem(eyebrowColorItem);

    beardColorItem = new UIMenuListItem("Color de vello facial", "Color del vello de facial de tu personaje", new ItemsCollection(hairColors));
    creatorHairMenu.AddItem(beardColorItem);

    eyeColorItem = new UIMenuListItem("Color de Ojos", "Color de ojos de tu personaje.", new ItemsCollection(Data.eyeColors));
    creatorHairMenu.AddItem(eyeColorItem);

    blushColorItem = new UIMenuListItem("Color de rubor", "Color de rubor de tu personaje.", new ItemsCollection(blushColors));
    creatorHairMenu.AddItem(blushColorItem);

    lipstickColorItem = new UIMenuListItem("Color de lápiz labial", "Color de lápiz labial de tu personaje", new ItemsCollection(lipstickColors));
    creatorHairMenu.AddItem(lipstickColorItem);

    chestHairColorItem = new UIMenuListItem("Color de pelo en el pecho", "Color de pelo en el pecho de tu personaje", new ItemsCollection(hairColors));
    creatorHairMenu.AddItem(chestHairColorItem);

    creatorHairMenu.AddItem(new UIMenuItem("Aleatorio", "~r~Randomiza tu pelo y color."));
    creatorHairMenu.AddItem(new UIMenuItem("Reiniciar", "~r~Reinicia tu pelo y color."));
}

function resetParentsMenu(refresh = false) {
    fatherItem.Index = 0;
    motherItem.Index = 0;
    similarityItem.Index = (currentGender == 0) ? 100 : 0;
    skinSimilarityItem.Index = (currentGender == 0) ? 100 : 0;

    updateParents();
    if (refresh) creatorParentsMenu.RefreshIndex();
}

function resetFeaturesMenu(refresh = false) {
    for (let i = 0; i < Data.featureNames.length; i++) {
        featureItems[i].Index = 100;
        updateFaceFeature(i);
    }

    if (refresh) creatorFeaturesMenu.RefreshIndex();
}

function resetAppearanceMenu(refresh = false) {
    for (let i = 0; i < Data.appearanceNames.length; i++) {
        appearanceItems[i].Index = 0;
        appearanceOpacityItems[i].Index = 100;
        updateAppearance(i);
    }

    if (refresh) creatorAppearanceMenu.RefreshIndex();
}

function resetHairAndColorsMenu(refresh = false) {
    hairItem.Index = 0;
    hairColorItem.Index = 0;
    hairHighlightItem.Index = 0;
    eyebrowColorItem.Index = 0;
    beardColorItem.Index = 0;
    eyeColorItem.Index = 0;
    blushColorItem.Index = 0;
    lipstickColorItem.Index = 0;
    chestHairColorItem.Index = 0;
    updateHairAndColors();

    if (refresh) creatorHairMenu.RefreshIndex();
}

let currentGender = 0;
let creatorMenus = [];
let creatorCamera;

// color arrays
let hairColors = [];
for (let i = 0; i < Data.maxHairColor; i++) hairColors.push(i.toString());

let blushColors = [];
for (let i = 0; i < Data.maxBlushColor; i++) blushColors.push(i.toString());

let lipstickColors = [];
for (let i = 0; i < Data.maxLipstickColor; i++) lipstickColors.push(i.toString());

// CREATOR MAIN
let creatorMainMenu = new Menu("Creador", "", new Point(50, 50));
let genderItem = new UIMenuListItem("Género", "~r~Cambiar esto reinicia tu personalización.", new ItemsCollection(["Hombre", "Mujer"]));
creatorMainMenu.AddItem(genderItem);
creatorMainMenu.AddItem(new UIMenuItem("Padres", "Los padres de tu personaje."));
creatorMainMenu.AddItem(new UIMenuItem("Características", "Las características faciales de tu personaje."));
creatorMainMenu.AddItem(new UIMenuItem("Apariencia", "La aparencia de tu personaje"));
creatorMainMenu.AddItem(new UIMenuItem("Pelo y Colores", "El pelo y color de pelo de tu personaje"));

let angles = [];
for (let i = -180.0; i <= 180.0; i += 5.0) angles.push(i.toFixed(1));
let angleItem = new UIMenuListItem("Ángulo", "", new ItemsCollection(angles));
creatorMainMenu.AddItem(angleItem);

let saveItem = new UIMenuItem("Guardar", "Guarda todos los cambios");
saveItem.BackColor = new Color(13, 71, 161);
saveItem.HighlightedBackColor = new Color(25, 118, 210);
creatorMainMenu.AddItem(saveItem);

creatorMainMenu.ListChange.on((item, listIndex) => {
    if (item == genderItem) {
        currentGender = listIndex;
        mp.events.callRemote("creator_GenderChange", listIndex);

        setTimeout(() => {
            localPlayer.clearTasksImmediately();
            applyCreatorOutfit();
            angleItem.Index = 0;
            resetParentsMenu(true);
            resetFeaturesMenu(true);
            resetAppearanceMenu(true);

            creatorHairMenu.Clear();
            fillHairMenu();
            creatorHairMenu.RefreshIndex();
        }, 200);
    } else if (item == angleItem) {
        localPlayer.setHeading(parseFloat(angleItem.SelectedValue));
        localPlayer.clearTasksImmediately();
    }
});



creatorMainMenu.ItemSelect.on((item, index) => {
    switch (index) {
        case 1:
            creatorMainMenu.Visible = false;
            creatorParentsMenu.Visible = true;
        break;

        case 2:
            creatorMainMenu.Visible = false;
            creatorFeaturesMenu.Visible = true;
        break;

        case 3:
            creatorMainMenu.Visible = false;
            creatorAppearanceMenu.Visible = true;
        break;

        case 4:
            creatorMainMenu.Visible = false;
            creatorHairMenu.Visible = true;
        break;

        case 6:
            let parentData = {
                Father: Data.fathers[fatherItem.Index],
                Mother: Data.mothers[motherItem.Index],
                Similarity: similarityItem.Index * 0.01,
                SkinSimilarity: skinSimilarityItem.Index * 0.01
            };

            let featureData = [];
            for (let i = 0; i < featureItems.length; i++) featureData.push(parseFloat(featureItems[i].SelectedValue));

            let appearanceData = [];
            for (let i = 0; i < appearanceItems.length; i++) appearanceData.push({Value: ((appearanceItems[i].Index == 0) ? 255 : appearanceItems[i].Index - 1), Opacity: appearanceOpacityItems[i].Index * 0.01});

            let hairAndColors = [
                Data.hairList[currentGender][hairItem.Index].ID,
                hairColorItem.Index,
                hairHighlightItem.Index,
                eyebrowColorItem.Index,
                beardColorItem.Index,
                eyeColorItem.Index,
                blushColorItem.Index,
                lipstickColorItem.Index,
                chestHairColorItem.Index
            ];

            mp.events.callRemote("creator_Save", currentGender, JSON.stringify(parentData), JSON.stringify(featureData), JSON.stringify(appearanceData), JSON.stringify(hairAndColors));
        break;

        case 7:
            mp.events.callRemote("creator_Leave");
        break;
    }
});

creatorMainMenu.MenuClose.on(() => {
    mp.events.callRemote("creator_Leave");
});

creatorMainMenu.Visible = false;
creatorMenus.push(creatorMainMenu);
// CREATOR MAIN END

// CREATOR PARENTS
let similarities = [];
for (let i = 0; i <= 100; i++) similarities.push(i + "%");

let creatorParentsMenu = new Menu("Padres", "", new Point(50, 50));
let fatherItem = new UIMenuListItem("Padre", "El padre de tu personaje.", new ItemsCollection(Data.fatherNames));
let motherItem = new UIMenuListItem("Madre", "La madre de tu personaje.", new ItemsCollection(Data.motherNames));
let similarityItem = new UIMenuListItem("Semejanza", "Semejanza de los padres\n(inferior = femenino, alto = masculino)", new ItemsCollection(similarities));
let skinSimilarityItem = new UIMenuListItem("Tono de piel", "Tono de piel similar a los padres.\n(inferior = femenino, alto = masculino)", new ItemsCollection(similarities));
creatorParentsMenu.AddItem(fatherItem);
creatorParentsMenu.AddItem(motherItem);
creatorParentsMenu.AddItem(similarityItem);
creatorParentsMenu.AddItem(skinSimilarityItem);
creatorParentsMenu.AddItem(new UIMenuItem("Aleatorio", "~r~Padres aleatorios."));
creatorParentsMenu.AddItem(new UIMenuItem("Reinicia", "~r~Reinicia tus padres."));

creatorParentsMenu.ItemSelect.on((item, index) => {
    switch (item.Text) {
        case "Aleatorio":
            fatherItem.Index = getRandomInt(0, Data.fathers.length - 1);
            motherItem.Index = getRandomInt(0, Data.mothers.length - 1);
            similarityItem.Index = getRandomInt(0, 100);
            skinSimilarityItem.Index = getRandomInt(0, 100);
            updateParents();
        break;

        case "Reinicia":
            resetParentsMenu();
        break;
    }
});

creatorParentsMenu.ListChange.on((item, listIndex) => {
    updateParents();
});

creatorParentsMenu.ParentMenu = creatorMainMenu;
creatorParentsMenu.Visible = false;
creatorMenus.push(creatorParentsMenu);
// CREATOR PARENTS END

// CREATOR FEATURES
let featureItems = [];
let features = [];
for (let i = -1.0; i <= 1.01; i += 0.01) features.push(i.toFixed(2));

let creatorFeaturesMenu = new Menu("Características", "", new Point(50, 50));

for (let i = 0; i < Data.featureNames.length; i++) {
    let tempFeatureItem = new UIMenuListItem(Data.featureNames[i], "", new ItemsCollection(features));
    tempFeatureItem.Index = 100;
    featureItems.push(tempFeatureItem);
    creatorFeaturesMenu.AddItem(tempFeatureItem);
}

creatorFeaturesMenu.AddItem(new UIMenuItem("Aleatorio", "~r~Características aleatorias"));
creatorFeaturesMenu.AddItem(new UIMenuItem("Reinicia", "~r~Reinicia tus características"));

creatorFeaturesMenu.ItemSelect.on((item, index) => {
    switch (item.Text) {
        case "Aleatorio":
            for (let i = 0; i < Data.featureNames.length; i++) {
                featureItems[i].Index = getRandomInt(0, 200);
                updateFaceFeature(i);
            }
        break;

        case "Reinicia":
            resetFeaturesMenu();
        break;
    }
});

creatorFeaturesMenu.ListChange.on((item, listIndex) => {
    updateFaceFeature(featureItems.indexOf(item));
});

creatorFeaturesMenu.ParentMenu = creatorMainMenu;
creatorFeaturesMenu.Visible = false;
creatorMenus.push(creatorFeaturesMenu);
// CREATOR FEATURES END

// CREATOR APPEARANCE
let appearanceItems = [];
let appearanceOpacityItems = [];
let opacities = [];
for (let i = 0; i <= 100; i++) opacities.push(i + "%");

let creatorAppearanceMenu = new Menu("Apariencia", "", new Point(50, 50));

for (let i = 0; i < Data.appearanceNames.length; i++) {
    let items = [];
    for (let j = 0, max = mp.game.ped.getNumHeadOverlayValues(i); j <= max; j++) items.push((Data.appearanceItemNames[i][j] === undefined) ? j.toString() : Data.appearanceItemNames[i][j]);

    let tempAppearanceItem = new UIMenuListItem(Data.appearanceNames[i], "", new ItemsCollection(items));
    appearanceItems.push(tempAppearanceItem);
    creatorAppearanceMenu.AddItem(tempAppearanceItem);

    let tempAppearanceOpacityItem = new UIMenuListItem(Data.appearanceNames[i] + " Opacidad", "", new ItemsCollection(opacities));
    tempAppearanceOpacityItem.Index = 100;
    appearanceOpacityItems.push(tempAppearanceOpacityItem);
    creatorAppearanceMenu.AddItem(tempAppearanceOpacityItem);
}

creatorAppearanceMenu.AddItem(new UIMenuItem("Aleatorio", "~r~Apariencia aleatoria."));
creatorAppearanceMenu.AddItem(new UIMenuItem("Reinicia", "~r~Reinicia tu apariencia."));

creatorAppearanceMenu.ItemSelect.on((item, index) => {
    switch (item.Text) {
        case "Aleatorio":
            for (let i = 0; i < Data.appearanceNames.length; i++) {
                appearanceItems[i].Index = getRandomInt(0, mp.game.ped.getNumHeadOverlayValues(i) - 1);
                appearanceOpacityItems[i].Index = getRandomInt(0, 100);
                updateAppearance(i);
            }
        break;

        case "Reinicia":
            resetAppearanceMenu();
        break;
    }
});

creatorAppearanceMenu.ListChange.on((item, listIndex) => {
    let idx = (creatorAppearanceMenu.CurrentSelection % 2 == 0) ? (creatorAppearanceMenu.CurrentSelection / 2) : Math.floor(creatorAppearanceMenu.CurrentSelection / 2);
    updateAppearance(idx);
});

creatorAppearanceMenu.ParentMenu = creatorMainMenu;
creatorAppearanceMenu.Visible = false;
creatorMenus.push(creatorAppearanceMenu);
// CREATOR APPEARANCE END

// CREATOR HAIR & COLORS
let hairItem;
let hairColorItem;
let hairHighlightItem;
let eyebrowColorItem;
let beardColorItem;
let eyeColorItem;
let blushColorItem;
let lipstickColorItem;
let chestHairColorItem;

creatorHairMenu = new Menu("Pelo y Colores", "", new Point(50, 50));
fillHairMenu();

creatorHairMenu.ItemSelect.on((item, index) => {
    switch (item.Text) {
        case "Aleatorio":
            hairItem.Index = getRandomInt(0, Data.hairList[currentGender].length - 1);
            hairColorItem.Index = getRandomInt(0, Data.maxHairColor);
            hairHighlightItem.Index = getRandomInt(0, Data.maxHairColor);
            eyebrowColorItem.Index = getRandomInt(0, Data.maxHairColor);
            beardColorItem.Index = getRandomInt(0, Data.maxHairColor);
            eyeColorItem.Index = getRandomInt(0, Data.maxEyeColor);
            blushColorItem.Index = getRandomInt(0, Data.maxBlushColor);
            lipstickColorItem.Index = getRandomInt(0, Data.maxLipstickColor);
            chestHairColorItem.Index = getRandomInt(0, Data.maxHairColor);
            updateHairAndColors();
        break;

        case "Reinicio":
            resetHairAndColorsMenu();
        break;
    }
});

creatorHairMenu.ListChange.on((item, listIndex) => {
    if (item == hairItem) {
        let hairStyle = Data.hairList[currentGender][listIndex];
        localPlayer.setComponentVariation(2, hairStyle.ID, 0, 2);
    } else {
        switch (creatorHairMenu.CurrentSelection) {
            case 1: // hair color
                localPlayer.setHairColor(listIndex, hairHighlightItem.Index);
            break;

            case 2: // hair highlight color
                localPlayer.setHairColor(hairColorItem.Index, listIndex);
            break;

            case 3: // eyebrow color
                localPlayer.setHeadOverlayColor(2, 1, listIndex, 0);
            break;

            case 4: // facial hair color
                localPlayer.setHeadOverlayColor(1, 1, listIndex, 0);
            break;

            case 5: // eye color
                localPlayer.setEyeColor(listIndex);
            break;

            case 6: // blush color
                localPlayer.setHeadOverlayColor(5, 2, listIndex, 0);
            break;

            case 7: // lipstick color
                localPlayer.setHeadOverlayColor(8, 2, listIndex, 0);
            break;

            case 8: // chest hair color
                localPlayer.setHeadOverlayColor(10, 1, listIndex, 0);
            break;
        }
    }
});

creatorHairMenu.ParentMenu = creatorMainMenu;
creatorHairMenu.Visible = false;
creatorMenus.push(creatorHairMenu);
// CREATOR HAIR & COLORS END



//-------------------------------------> empezamos <-----------------------------------------


mp.events.add("CambiarGenero", (id) => { // <------------------------------------------------------------------
  currentGender = id;
  mp.events.callRemote("creator_GenderChange", id);

          setTimeout(() => {
              localPlayer.clearTasksImmediately();
              applyCreatorOutfit();
              angleItem.Index = 0;
              resetParentsMenu(true);
              resetFeaturesMenu(true);
              resetAppearanceMenu(true);

              creatorHairMenu.Clear();
              fillHairMenu();
              creatorHairMenu.RefreshIndex();
          }, 200);
});

mp.events.add("RdmPadres", (tipo) => { // <------------------------------------------------------------------

    switch (tipo) {
        case "Aleatorio":
            fatherItem.Index = getRandomInt(0, Data.fathers.length - 1);
            motherItem.Index = getRandomInt(0, Data.mothers.length - 1);
            similarityItem.Index = getRandomInt(0, 100);
            skinSimilarityItem.Index = getRandomInt(0, 100);
            updateParents();
        break;

        case "Reinicia":
            resetParentsMenu();
        break;
    }
});

mp.events.add("GenesPadres", (genPat,genMat,faceMix,skinMix) => { // <------------------------------------------------------------------
    //let asd = typeof(genPat);
    //creadorCef.execute(`logeo('RAGEEEEEEEEEEE: Camara ------'+"${asd}"+"${genPat}"+"${faceMix}")`);
    fatherItem.Index = genPat;
    motherItem.Index = genMat;
    similarityItem.Index = faceMix;
    skinSimilarityItem.Index = skinMix;
    updateParents();
});


mp.events.add("RdmCara", (tipo) => { // <------------------------------------------------------------------
    switch (tipo) {
        case "Aleatorio":
            for (let i = 0; i < Data.featureNames.length; i++) {
                featureItems[i].Index = getRandomInt(0, 200);
                updateFaceFeature(i);
            }
        break;

        case "Reinicia":
            resetFeaturesMenu();
        break;
    }
});


mp.events.add("SetCaraC", (tipo,valor) => { // <------------------------------------------------------------------
  //creadorCef.execute(`logeo('RAGE: SetCaraC ------'+"${tipo}"+"${valor}")`);
  featureItems[tipo].Index = valor;
  updateFaceFeature(tipo);
});


mp.events.add("RdmApariencia", (tipo) => { // <------------------------------------------------------------------
    switch (tipo) {
        case "Aleatorio":
            for (let i = 0; i < Data.appearanceNames.length; i++) {
                appearanceItems[i].Index = getRandomInt(0, mp.game.ped.getNumHeadOverlayValues(i) - 1);
                appearanceOpacityItems[i].Index = getRandomInt(0, 100);
                updateAppearance(i);
            }
        break;

        case "Reinicia":
            resetAppearanceMenu();
        break;
    }
});

mp.events.add("SetApariencia", (tipo,valor) => { // <------------------------------------------------------------------
    appearanceItems[tipo].Index = valor; 
    appearanceOpacityItems[tipo].Index = 100;
    updateAppearance(tipo); // 2
});


mp.events.add("RdmColores", (tipo) => { // <------------------------------------------------------------------
    switch (tipo) {
        case "Aleatorio":
            hairItem.Index = getRandomInt(0, Data.hairList[currentGender].length - 1);
            hairColorItem.Index = getRandomInt(0, Data.maxHairColor);
            hairHighlightItem.Index = getRandomInt(0, Data.maxHairColor);
            eyebrowColorItem.Index = getRandomInt(0, Data.maxHairColor);
            beardColorItem.Index = getRandomInt(0, Data.maxHairColor);
            eyeColorItem.Index = getRandomInt(0, Data.maxEyeColor);
            blushColorItem.Index = getRandomInt(0, Data.maxBlushColor);
            lipstickColorItem.Index = getRandomInt(0, Data.maxLipstickColor);
            chestHairColorItem.Index = getRandomInt(0, Data.maxHairColor);
            updateHairAndColors();
        break;

        case "Reinicia":
            resetHairAndColorsMenu();
        break;
    }
});


mp.events.add("SetColores", (peloTipo,peloCol,peloIlu,cejasCol,barbaCol,ojosCol,ruborCol,labiosCol,pechoCol) => { // <------------------------------------------------------------------

  hairItem.Index = peloTipo;
  hairColorItem.Index = peloCol;
  hairHighlightItem.Index = peloIlu;
  eyebrowColorItem.Index = cejasCol;
  beardColorItem.Index = barbaCol;
  eyeColorItem.Index = ojosCol;
  blushColorItem.Index = ruborCol;
  lipstickColorItem.Index = labiosCol;
  chestHairColorItem.Index = pechoCol;
  updateHairAndColors();

});

mp.events.add('SelRopaPj', (data) => {
  data = JSON.parse(data);
  if (data.torso != undefined) {
    character_data.torso = data.torso;
    mp.events.callRemote('SetRopaPj', 3, data.torso, 0);
  }
  if (data.undershirt != undefined) {
    character_data.undershirt = data.undershirt;
    mp.events.callRemote('SetRopaPj', 8, data.undershirt, 0);
  }
  if (data.slot == 4)
    character_data.legs = data.variation;
  else if (data.slot == 6)
    character_data.feet = data.variation;
  else if (data.slot == 7)
    character_data.accessory = data.variation;
  else if (data.slot == 8)
    character_data.undershirt = data.variation;
  else if (data.slot == 11) {
    character_data.topshirt = data.variation;
    character_data.topshirtTexture = data.texture;
  }
  mp.events.callRemote('SetRopaPj', data.slot, data.variation, data.texture);
});

mp.events.add("guardarCreador", () => { // <------------------------------------------------------------------
    
  let parentData = {
      Father: Data.fathers[fatherItem.Index],
      Mother: Data.mothers[motherItem.Index],
      Similarity: similarityItem.Index * 0.01,
      SkinSimilarity: skinSimilarityItem.Index * 0.01
  };

  let featureData = [];
  for (let i = 0; i < featureItems.length; i++) featureData.push(parseFloat(featureItems[i].SelectedValue));

  let appearanceData = [];
  for (let i = 0; i < appearanceItems.length; i++) appearanceData.push({Value: ((appearanceItems[i].Index == 0) ? 255 : appearanceItems[i].Index - 1), Opacity: appearanceOpacityItems[i].Index * 0.01});

  let hairAndColors = [
      Data.hairList[currentGender][hairItem.Index].ID,
      hairColorItem.Index,
      hairHighlightItem.Index,
      eyebrowColorItem.Index,
      beardColorItem.Index,
      eyeColorItem.Index,
      blushColorItem.Index,
      lipstickColorItem.Index,
      chestHairColorItem.Index
  ];

  if (creadorCef) {
      creadorCef.destroy();
      creadorCef = undefined;
  } 
  mp.gui.cursor.visible = false;
  mp.game.cam.renderScriptCams(false, false, 0, true, false);

  mp.events.callRemote("creator_Save", currentGender, JSON.stringify(parentData), JSON.stringify(featureData), JSON.stringify(appearanceData), JSON.stringify(hairAndColors),JSON.stringify(character_data));

});
  
mp.events.add("salirCreador", () => { // <------------------------------------------------------------------
  if (creadorCef) {
      creadorCef.destroy();
      creadorCef = undefined;
  } 
  mp.gui.cursor.visible = false;
  mp.game.cam.renderScriptCams(false, false, 0, true, false);

  mp.events.callRemote("creator_Leave");  
});


mp.events.add("toggleCreator", (active, charData) => {    
    if (active) {
        if (creatorCamera === undefined) {
            creatorCamera = mp.cameras.new("creatorCamera", creatorCoords.camera, new mp.Vector3(0, 0, 0), 45);
            creatorCamera.pointAtCoord(402.8664, -996.4108, -98.5);
            creatorCamera.setActive(true);            
        }
        if (!creadorCef) {
            creadorCef = mp.browsers.new("package://statics/pj/creator.html");                
            }        
        // update menus with current character data
        if (charData) {
            charData = JSON.parse(charData);

            // gender
            currentGender = charData.Gender;
            genderItem.Index = charData.Gender;

            creatorHairMenu.Clear();
            fillHairMenu();
            applyCreatorOutfit();

            // parents
            fatherItem.Index = Data.fathers.indexOf(charData.Parents.Father);
            motherItem.Index = Data.mothers.indexOf(charData.Parents.Mother);
            similarityItem.Index = parseInt(charData.Parents.Similarity * 100);
            skinSimilarityItem.Index = parseInt(charData.Parents.SkinSimilarity * 100);
            updateParents();

            // features
            for (let i = 0; i < charData.Features.length; i++) {
                featureItems[i].Index = (charData.Features[i] * 100) + 100;
                updateFaceFeature(i);
            }

            // hair and colors
            let hair = Data.hairList[currentGender].find(h => h.ID == charData.Hair.Hair);
            hairItem.Index = Data.hairList[currentGender].indexOf(hair);

            hairColorItem.Index = charData.Hair.Color;
            hairHighlightItem.Index = charData.Hair.HighlightColor;
            eyebrowColorItem.Index = charData.EyebrowColor;
            beardColorItem.Index = charData.BeardColor;
            eyeColorItem.Index = charData.EyeColor;
            blushColorItem.Index = charData.BlushColor;
            lipstickColorItem.Index = charData.LipstickColor;
            chestHairColorItem.Index = charData.ChestHairColor;
            updateHairAndColors();

            // appearance
            for (let i = 0; i < charData.Appearance.length; i++) {
                appearanceItems[i].Index = (charData.Appearance[i].Value == 255) ? 0 : charData.Appearance[i].Value + 1;
                appearanceOpacityItems[i].Index = charData.Appearance[i].Opacity * 100;
                updateAppearance(i);
            }
        }

        for (let i = 0; i < creatorMenus.length; i++) creatorMenus[i].Visible = false;
        mp.gui.chat.show(false);
        mp.game.ui.displayRadar(false);
        mp.game.ui.displayHud(false);
        localPlayer.clearTasksImmediately();
        localPlayer.freezePosition(true);
        mp.gui.cursor.show(true, true);
        mp.game.cam.renderScriptCams(true, false, 0, true, false);
        setTimeout(() => {mp.gui.cursor.visible = true},1000);
    } else {
        for (let i = 0; i < creatorMenus.length; i++) creatorMenus[i].Visible = false;
        mp.gui.chat.show(true);
        mp.gui.cursor.visible = false;
        mp.game.ui.displayRadar(true);
        mp.game.ui.displayHud(true);
        mp.gui.cursor.show(false, false);
        localPlayer.freezePosition(false);
        //localPlayer.setDefaultComponentVariation();
        //localPlayer.setComponentVariation(2, Data.hairList[currentGender][hairItem.Index].ID, 0, 2);


        mp.game.cam.renderScriptCams(false, false, 0, true, false);
    }
});



mp.events.add('MoveCameraPosition', (pos) => {
  //browser.execute(`logeo('RAGEEEEEEEEEEE: Camara ------'+"${pos}")`);
  if (creatorCamera)  {creatorCamera.destroy()}
  switch (pos) {
    case 0:
      {
        // Head
        var camPos = new mp.Vector3(402.9378, -997.0, -98.35);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        creatorCamera = mp.cameras.new('lookAtHead', camPos, camRot, 40);
        creatorCamera.pointAtCoord(402.9198, -996.5348, -98.35);
        creatorCamera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 1:
      {
        // Torso
        var camPos = new mp.Vector3(402.9378, -997.5, -98.60);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        creatorCamera = mp.cameras.new('lookAtTorso', camPos, camRot, 40);
        creatorCamera.pointAtCoord(402.9198, -996.5348, -98.60);
        creatorCamera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 2:
      {
        // Legs
        var camPos = new mp.Vector3(402.9378, -997.5, -99.40);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        creatorCamera = mp.cameras.new('lookAtLegs', camPos, camRot, 40);
        creatorCamera.pointAtCoord(402.9198, -996.5348, -99.40);
        creatorCamera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    case 3:
      {
        // Feet
        var camPos = new mp.Vector3(402.9378, -997.5, -99.85);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        creatorCamera = mp.cameras.new('lookAtFeet', camPos, camRot, 40);
        creatorCamera.pointAtCoord(402.9198, -996.5348, -99.85);
        creatorCamera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
    default:
      {
        // Default
        var camPos = new mp.Vector3(403.6378, -998.5422, -99.00404);
        var camRot = new mp.Vector3(0.0, 0.0, 176.891);

        creatorCamera = mp.cameras.new('lookAtBody', camPos, camRot, 40);
        creatorCamera.pointAtCoord(402.9198, -996.5348, -99.00024);
        creatorCamera.setActive(true);

        mp.game.cam.renderScriptCams(true, false, 2000, true, false);
        break;
      }
  }
});


mp.events.add('AccionCamara', (data) => {
  data = JSON.parse(data);
  //browser.execute(`logeo('RAGEEEEEEEEEEE: AccionCamara ------'+"${data.xx}---${data.yy}")`);
  if (creatorCamera)  {creatorCamera.setCoord(403.6378, data.xx, data.yy)}
});

mp.events.add('AccionCamaraRot', (data) => {
  data = JSON.parse(data);
  //browser.execute(`logeo('RAGEEEEEEEEEEE: AccionCamara ------'+"${data.zz}")`);
  if (creatorCamera)  {creatorCamera.setRot(0.0, 0.0, data.zz, 2);}
  mp.players.local.setRotation(0.0, 0.0, data.zz, 2, true);
});



mp.keys.bind(0x78, true, function() {   // F9 temporal
    mp.gui.cursor.visible = true;
});