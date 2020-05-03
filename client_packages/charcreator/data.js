const fathers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44];
const mothers = [21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 45];
const fatherNames = ["Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", "Claude", "Niko", "John"];
const motherNames = ["Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet", "Sophia", "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma", "Misty"];
const featureNames = ["Ancho", "Altura del fondo", "Longitud de la punta", "Profundidad", "Altura de la punta", "Nariz Rota", "Altura de Cejas", "Profundidad de Cejas", "Altura del Pómulo", "Ancho del Pómulo", "Cheek Depth", "Profundidad de la Mejilla", "Grosor del Labio", "Ancho de la Mandíbula", "Forma de la Mandíbula", "Altura de la Barbilla", "Profundidad de la Barbilla", "Ancho de la Barbilla", "Grosor de Mentón", "Anchura del Cuello"];
const appearanceNames = ["Manchas", "Vello Facial", "Cejas", "Envejecimiento", "Maquillaje", "Sonrojo", "Rostro", "Daño del sol", "Lápiz Labial", "Lunares y Pecas", "Pelo del Pecho"];

const appearanceItemNames = [
    // Manchas
    ["Ninguno", "Sarampión", "Espinillas", "Manchas", "Fugas", "Espinillas", "Construcción", "Pústulas", "Zits", "Acné Completo", "Acné", "Erupción en la Mejilla", "Erupción Facial", "Recogedor", "Pubertad", "Monstruosidad", "Erupción en la Barbilla", "Dos caras", "Zona T", "Grasa", "Marcado", "Cicatrices de Acné", "Cicatrices de Acné completas", "Herpes Labial", "Impétigo"],
    // Vello Facial
    ["Ninguno", "Rastrojo Ligero", "Balbo", "Barba Circular", "Barba de Chivo", "Barbilla", "Barba con Pelusa", "Correa de Mentón", "Desaliñada", "Mosquetero", "Mostacho", "Barba Recortada", "Rastrojo", "Barba Circular delgada", "Herradura", "Lápiz de Labios", "Barba Perfilada", "Balbo y Patillas", "Labios", "Barba Desaliñada", "Rizada", "Rizado profundo y extraño", "Barba Manillar", "Faustico", "Otón y Parche", "Otón extraña y completa", "Franz claro", "Hampstead", "La ambrosidad", "Cortina Lincoln"],
    // Cejas
    ["Ninguno", "Balanceada", "De moda", "Cleopatra", "Burlona", "Femenina", "Seductiva", "Apretada", "Chola", "Triunfada", "Despreocupada", "Curvilínea", "Roedora", "Doble Tranvía", "Delgadas", "A lápiz", "Desplumadas", "Recta y estrecha", "Natural", "Borrosa", "Desaliñada", "Oruga", "Regular", "Mediterránea", "Arreglada", "Bushels", "Plumadas", "Espinosas", "Monobrow", "Aladas", "Tranvía Triple", "Tranvía arqueado", "Recortes", "Desvanecidas", "Tranvía Solo"],
    // Envejecimiento
    ["Ninguno", "Joven", "Primeros Signos", "Edad Mediana", "Signos de Preocupación", "Depresión", "Distinguido", "Envejecido", "Resistido", "Arrugado", "Hundido", "Vida Difícil", "Clásico", "Retirado", "Drogadicto", "Geriátrico"],
    // Maquillaje
    ["Ninguno", "Negro Ahumado", "Bronceado", "Gris Suave", "Glamour Retro", "Look Natural", "Ojos de Gato", "Chola", "Vamp", "Glamour de Vinewood", "Chicle", "Sueño Acuático", "Fijado", "Pasión Púrpura", "Ojos de gato Ahumados", "Rubí Humeante", "Princesa Pop"],
    // Sonrojo
    ["Ninguno", "Completo", "Angular", "Redondo", "Horizontal", "Alto", "Corazón", "Antiguo"],
    // Tez
    ["Ninguno", "Mejillas Sonrojadas", "Rastrojo", "Sofocos", "Bronceado", "Magullado", "Alcoholico", "Irregular", "Tótem", "Vasos Sanguineos", "Dañado", "Pálido", "Fantasmal"],
    // Daño del Sol
    ["Ninguno", "Desigual", "Papel de Lija", "Irregular", "Áspero", "Correoso", "Texturizado", "Gruesa", "Escabroso", "Arrugado", "Agrietado", "Arenoso"],
    // Lápiz Labial
    ["Ninguno", "Color Mate", "Color Brillante", "Mate Lineado", "Brillo Forrado", "Mate con Forro Grueso", "Brillo forrado pesado", "Mate desnudo alineado", "Trazado desnudo", "Manchado", "Geisha"],
    // Pecas
    ["Ninguno", "Querubín", "Lleno", "Irregular", "Punto guión", "Sobre el puente", "Picardias", "Duendecito", "Besado por el sol", "Marcas de Belleza", "En fila", "Modelesco", "Ocasional", "Moteado", "Gotas de Lluvia", "Doble carga", "Unilateral", "Pares", "Crecimiento"],
    // Pelo del pecho
    ["Ninguno", "Natural", "La tira", "El arbol", "Peludo", "Espeluznante", "Mono", "Mono preparado", "Bikini", "Rayo", "Rayo inverso", "Corazón", "Chestache", "Cara feliz", "Calavera", "Serpiente", "Pellizcos", "Brazos Velludos"]
];

const hairList = [
    // Masculino
    [
        {ID: 0, Name: "Afeitado apurado", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz"},
        {ID: 1, Name: "Buzzcut", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_001"},
        {ID: 2, Name: "Halcón", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002"},
        {ID: 3, Name: "Hipster", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_003"},
        {ID: 4, Name: "Despedida Lateral", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_004"},
        {ID: 5, Name: "Corto", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_005"},
        {ID: 6, Name: "Biker", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_006"},
        {ID: 7, Name: "Cola de Caballo", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_007"},
        {ID: 8, Name: "Trenzas", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_008"},
        {ID: 9, Name: "Kanox", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_009"},
        {ID: 10, Name: "Cepillado", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_013"},
        {ID: 11, Name: "Osvaldo", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002"},
        {ID: 12, Name: "Caesar", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_011"},
        {ID: 13, Name: "Cortado", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_012"},
        {ID: 14, Name: "Teme", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014"},
        {ID: 15, Name: "Pelo Lago", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015"},
        {ID: 16, Name: "Rizos Peludos", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_000"},
        {ID: 17, Name: "Surfista", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_001"},
        {ID: 18, Name: "Parte Corta", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_000"},
        {ID: 19, Name: "Lados altos", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_001"},
        {ID: 20, Name: "Slicked Largo", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_000"},
        {ID: 21, Name: "Hipster Joven", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_001"},
        {ID: 22, Name: "Mójol", Collection: "multiplayer_overlays", Overlay: "NGInd_M_Hair_000"},
        {ID: 24, Name: "Cornrowns clásico", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_000"},
        {ID: 25, Name: "Cornrowns de palma", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_001"},
        {ID: 26, Name: "Cornrowns relámpago", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_002"},
        {ID: 27, Name: "Cornrowns azotado", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_003"},
        {ID: 28, Name: "Cornrowns zig zag", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_004"},
        {ID: 29, Name: "Cornrowns caracol", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_005"},
        {ID: 30, Name: "Cima más alta", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_006"},
        {ID: 31, Name: "Barrido hacia atrás", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_M"},
        {ID: 32, Name: "Socavado barrido hacia atrás", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_M"},
        {ID: 33, Name: "Barrido socavado", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_M"},
        {ID: 34, Name: "Mohawk claveteado", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_M"},
        {ID: 35, Name: "Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_M"},
        {ID: 36, Name: "Mod en capas", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_005_M"},
        {ID: 72, Name: "Superficie plana", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_000_M"},
        {ID: 73, Name: "Corte Militar", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_001_M"}
    ],
    // Femenino
    [
        {ID: 0, Name: "Apurado", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz"},
        {ID: 1, Name: "Corto", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_001"},
        {ID: 2, Name: "Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_002"},
        {ID: 3, Name: "Coletas", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003"},
        {ID: 4, Name: "Cola de caballo", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_004"},
        {ID: 5, Name: "Mohawk trenzado", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_005"},
        {ID: 6, Name: "Trenzas", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_006"},
        {ID: 7, Name: "Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007"},
        {ID: 8, Name: "Halcón imitado", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_008"},
        {ID: 9, Name: "Toque francés", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_009"},
        {ID: 10, Name: "Largo Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_010"},
        {ID: 11, Name: "Flojo atado", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_011"},
        {ID: 12, Name: "Duendecito", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_012"},
        {ID: 13, Name: "Flequillo", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_013"},
        {ID: 14, Name: "Moño", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014"},
        {ID: 15, Name: "Bob Ondulado", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015"},
        {ID: 16, Name: "Moño desordenado", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_000"},
        {ID: 17, Name: "Pin Up chica", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001"},
        {ID: 18, Name: "Moño apretado", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007"},
        {ID: 19, Name: "Bob torcido", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_000"},
        {ID: 20, Name: "Bob Flapper", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_001"},
        {ID: 21, Name: "Big Bangs", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001"},
        {ID: 22, Name: "Nudo Superior trenzado", Collection: "multiplayer_overlays", Overlay: "NGHip_F_Hair_000"},
        {ID: 23, Name: "Mójol", Collection: "multiplayer_overlays", Overlay: "NGInd_F_Hair_000"},
        {ID: 25, Name: "Cornrows pellizcados", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_000"},
        {ID: 26, Name: "Cornrows de hoja", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_001"},
        {ID: 27, Name: "Cornrows zig zag", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_002"},
        {ID: 28, Name: "Flequillo de coleta", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003"},
        {ID: 29, Name: "Trenzas onduladas", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003"},
        {ID: 30, Name: "Trenzas de la bobina", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_004"},
        {ID: 31, Name: "Quiff enrollado", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_006"},
        {ID: 32, Name: "Suelto barrido hacia atrás", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_F"},
        {ID: 33, Name: "Socavado barrido hacia atrás", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_F"},
        {ID: 34, Name: "Lado barrido socavado", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_F"},
        {ID: 35, Name: "Mohawk claveteado", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_F"},
        {ID: 36, Name: "Bandana y trenza", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003"},
        {ID: 37, Name: "Mod en capas", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_006_F"},
        {ID: 38, Name: "Skinbyrd", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_F"},
        {ID: 76, Name: "Bollo aseado", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_000_F"},
        {ID: 77, Name: "Bob corto", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_001_F"}
    ]
];

const eyeColors = ["Verdes", "Esmeralda", "Azul claro", "Azul oceánico", "Marrón claro", "Marrón oscuro", "Avellana", "Gris oscuro", "Gris claro", "Rosa", "Amarillo", "Morado", "Apagados", "Con sombras", "Tequila Amanecer", "Atómicos", "Deformado", "ECola", "Guardián Espacial", "Ying Yang", "Diana", "Lagartija", "Dragón", "Extra Terrestre", "Cabra", "Sonriente", "Poseido", "Demonio", "Infectado", "Alien", "Muerto Viviente", "Zombie"];

exports = {
    fathers: fathers,
    mothers: mothers,
    fatherNames: fatherNames,
    motherNames: motherNames,
    featureNames: featureNames,
    appearanceNames: appearanceNames,
    appearanceItemNames: appearanceItemNames,
    hairList: hairList,
    eyeColors: eyeColors,
    maxHairColor: 64,
    maxEyeColor: 32,
    maxBlushColor: 27,
    maxLipstickColor: 32
};