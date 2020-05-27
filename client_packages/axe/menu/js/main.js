'use strict';

var menuItems = [
    {
        id   : 'config',
        title: 'Configuración',
        icon: '#config'
    },    
    {
        id   : 'anim',
        title: 'Animaciones...',
        icon: '#anim',
        items: []
    },    
    {
        id   : 'info',
        title: 'Info PJ',
        icon: '#info'
    },    
    {
        id   : 'caminata',
        title: 'Estilo...',
        icon: '#estilo',
        items: [{
            id   : 'cam0',
            title: 'Normal',
            icon: '#estilo'
        },
        {
            id   : 'cam1',
            title: 'Valiente',
            icon: '#estilo'
        }
        ,
        {
            id   : 'cam2',
            title: 'Confiado',
            icon:'#estilo'
        }
        ,
        {
            id   : 'cam3',
            title: 'Borracho',
            icon: '#estilo'
        },
        {
            id   : 'cam4',
            title: 'Obeso',
            icon: '#estilo'
        },
        {
            id   : 'cam5',
            title: 'Ganster',
            icon: '#estilo'
        },
        {
            id   : 'cam6',
            title: 'Apurado',
            icon: '#estilo'
        },
        {
            id   : 'cam7',
            title: 'Borracho',
            icon: '#estilo'
        },
        {
            id   : 'cam8',
            title: 'Herido',
            icon: '#estilo'
        },
        {
            id   : 'cam9',
            title: 'Miedo',
            icon: '#estilo'
        },
        {
            id   : 'cam10',
            title: 'Triste',
            icon: '#estilo'
        },
        {
            id   : 'cam11',
            title: 'Rudo',
            icon: '#estilo'
        }
      ]
    },
    {
        id   : 'jetas',
        title: 'Estado...',
        icon: '#jetas',
        items: [{
            id   : 'jet0',
            title: 'Normal',
            icon: '#jetas'
        },{
            id   : 'jet1',
            title: 'Apuntado',
            icon: '#jetas'
        },
        {
            id   : 'jet2',
            title: 'Molesto',
            icon: '#jetas'
        },
        {
            id   : 'jet3',
            title: 'Ebrio',
            icon: '#jetas'
        },
        {
            id   : 'jet4',
            title: 'Feliz',
            icon: '#jetas'
        },
        {
            id   : 'jet5',
            title: 'Herido',
            icon: '#jetas'
        },
        {
            id   : 'jet6',
            title: 'Estrés',
            icon: '#jetas'
        },
        {
            id   : 'jet7',
            title: 'Enojado',
            icon: '#jetas'
        }
        ]
    },
    {
        id   : 'coche',
        title: 'Coche',
        icon: '#coche'
    }
];
    var svgMenu;
    var arrbindeos = [];
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    function inicioMenu() {
    svgMenu = new RadialMenu({
        parent      : pantalla,
        size        : 500,
        closeOnClick: true,
        menuItems   : menuItems,
        onClick     : function (item) {
            //console.log('You have clicked:', item.id, item.title);
            
            if (item.title == "animacion"){
                svgMenu.close();
                mp.trigger("SalirMenuPj");
                mp.trigger("IniciarAnimacion",item.id);
                //console.log("iconoooo - > "+item.icon);
            }else{
            svgMenu.close();
            mp.trigger("SalirMenuPjSoft");
                switch (item.id){
                    case "config":
                        mp.trigger("AbrirConfig");
                    break;
                    case "info":                        
                        mp.trigger("AbrirInfo");
                    break;
                    case "mas":                        
                        mp.trigger("MasAnimaciones");
                    break;
                    case "cam0":
                        mp.trigger("SetEstiloCaminar",0);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam1":
                        mp.trigger("SetEstiloCaminar",1);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam2":
                        mp.trigger("SetEstiloCaminar",2);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam3":
                        mp.trigger("SetEstiloCaminar",3);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam4":
                        mp.trigger("SetEstiloCaminar",4);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam5":
                        mp.trigger("SetEstiloCaminar",5);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam6":
                        mp.trigger("SetEstiloCaminar",6);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam7":
                        mp.trigger("SetEstiloCaminar",7);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam8":
                        mp.trigger("SetEstiloCaminar",8);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam9":
                        mp.trigger("SetEstiloCaminar",9);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam10":
                        mp.trigger("SetEstiloCaminar",10);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "cam11":
                        mp.trigger("SetEstiloCaminar",11);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "jet0":
                        mp.trigger("jetas",0);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet1":
                        mp.trigger("jetas",1);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet2":
                        mp.trigger("jetas",2);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet3":
                        mp.trigger("jetas",3);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet4":
                        mp.trigger("jetas",4);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet5":
                        mp.trigger("jetas",5);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet6":
                        mp.trigger("jetas",6);
                        mp.trigger("SalirMenuPj");
                    break;
                     case "jet7":
                        mp.trigger("jetas",7);
                        mp.trigger("SalirMenuPj");
                    break;
                    case "coche":
                        mp.trigger("menuCoche");
                        mp.trigger("SalirMenuPjSoft");
                    break;

                }

            }            
        }
    });

    }

    function animaciones(arr){
        //console.log(arr);
    var parsed = JSON.parse(arr); //var parsed = JSON.parse('["Parar Anim","AplaudirM","Avisar por radio","Levantar Mano","Grabar mobil","Timida","Brazos cruzadosM","Tranquilo tranquilo","¿Donde estoy?","¿Que es eso?","AplaudirF","Aplaudir sin ganas","Apoyarse en la pared","Apoyarse manos juntas","Apuntar en papel","Arreglar bombilla","Brazos cruzadosF","Borracho 1","Borracho 2","Borracho 3","Celebrar","Con ritmoM","Con ritmoF","Charlar","Decepcion","Depresion","Estirar musculos","Flexion","Fumar apoyado 1","Fumar apoyado 2","Fumar apoyado 3","Fumar apoyado 4","Fumar apoyado 5","Fumar tranquilo","Fumar tranquila","Limpiar manos agachado","Me duele la espalda","Mirar el suelo","Nervioso","Posar","SentarseM","SentarseF","Sentarse pies cruzados","Sentarse piernas cruzadas","Sentarse con pie fuera","Sentarse tímido","Sentarse cansado 1","Sentarse cansado 2","Sentarse apoyado","Sentarse con musica","Sentarse a disfrutar 1","Sentarse a disfrutar 2","Sentarse a disfrutar 3","Sentarse a disfrutar 4","Sentarse piernas abiertas","Vigilar el area"]');
    var neo = [];
    var i = 0;

    var findarr = menuItems.find(element => element.id == 'anim');

    parsed.forEach(elemento => {
      if (i < 7) {
            neo.push(
               {id: i, title: 'animacion', icon: '#'+elemento}
            );
            i++
        }
    });
    neo.push(
               {id: "mas", title: "Todas", icon: "#mas"}
            );
    //[{id: 'firearm',icon: '#firearm',title: 'Firearm...',items: [{}]}]

    findarr.items = neo;
    inicioMenu()
    }


    function bindeos(arr){
        arrbindeos = JSON.parse(arr);
    }

    document.addEventListener('keydown', (event) => {
    reCalculate(event)
    }); 

    function reCalculate(e){
        //console.log("solo ecode->"+e.keyCode);
        arrbindeos.forEach(bind => {
            if (e.keyCode == bind){
                //console.log(e.keyCode +" == "+ bind);
                mp.trigger("IniciarAnimacion",arrbindeos.indexOf(bind));
            }
        });  
    }

    function toggle(boleano){
        if (boleano){
            svgMenu.open()
        }else{
            svgMenu.close();
        }
    }

    function alerta(){
        //$("#playAudio").play()
        var x = document.getElementById("playAudio");
        x.play();
    }

    function sonido(url,segundos){
        //var arg = JSON.parse(args);
        //var url = args;
        //var segundos = 0;

        var secs = parseInt(segundos) * 1000;
        console.log(url+" secs: "+secs);
        var audio = document.getElementById('sonido');
        var source = document.getElementById('audioSource');
        if (!audio.paused){
            audio = document.getElementById('sonido2');
            source = document.getElementById('audioSource2');
        }
        if (!audio.paused){
            audio = document.getElementById('sonido3');
            source = document.getElementById('audioSource3');
        }
        
        source.src = url;

        if (secs > 0){
            audio.loop=true;
            setTimeout(function(){ 
                audio.pause();
                //audio.currentTime = 0;
            }, secs);
        }

        audio.load();
        audio.play();
    }

