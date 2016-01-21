﻿-- TEMPLATES
insert into Templates (Id, Name) 
values ('16284653806660-fda6c4c1ae034e5ea', 'Coat of Arms')

-- DESIGN REGIONS
insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16284777552027-e031b206be8445088', 'Shield', 160, 350, 260, 340, 1, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16284803596595-4f3a31b335364a098', 'Crest', 226, 3, 128, 140, 2, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16284824297863-52e9496edf474f5c8', 'Mantelling', 113, 112, 128, 140, 3, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16285529899501-d28f80fb18a7411cb', 'Helm', 205, 185, 170, 155, 4, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16285827830074-ad202fb38c614f689', 'Coronet', 170, 227, 240, 105, 5, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16286103410954-06c1e844492a4438a', 'LeftSupporter', 24, 327, 188, 365, 6, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16286326311881-9b53368169a34e299', 'RightSupporter', 368, 327, 188, 365, 7, '16284653806660-fda6c4c1ae034e5ea')

insert into DesignRegions(Id, Name, X, Y, Width, Height, SortOrder, TemplateId)
values ('16286544643326-dbe98aecd80b4a348', 'Motto', 78, 707, 424, 76, 8, '16284653806660-fda6c4c1ae034e5ea')

-- COMPATIBILITY TAGS
insert into CompatibilityTags(Id, Tag)
values ('16287451685237-41dc3078073b43489', 'Shield')

insert into CompatibilityTags(Id, Tag)
values ('16287561135042-9a39cf27cdc948b5b', 'Crest')

insert into CompatibilityTags(Id, Tag)
values ('16287597150680-07e049face434fbfb', 'Mantelling')

insert into CompatibilityTags(Id, Tag)
values ('16287784665352-62869ae6958e4deea', 'Helm')

insert into CompatibilityTags(Id, Tag)
values ('16287823428798-e87b224c697e4e878', 'Coronet')

insert into CompatibilityTags(Id, Tag)
values ('16287860489062-1944ba15018e4787b', 'Supporter')

insert into CompatibilityTags(Id, Tag)
values ('16287889574439-1a0f21d3bf784920a', 'Motto')

insert into CompatibilityTags(Id, Tag)
values ('16288158401819-bd11d676ff0f4e5da', 'Stamp')

-- DESIGN REGION COMPATIBILITY TAGS
-- Shield | Shield
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16284777552027-e031b206be8445088|16287451685237-41dc3078073b43489', '16284777552027-e031b206be8445088', '16287451685237-41dc3078073b43489')

-- Crest | Crest
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16284803596595-4f3a31b335364a098|16287561135042-9a39cf27cdc948b5b', '16284803596595-4f3a31b335364a098', '16287561135042-9a39cf27cdc948b5b')

-- Mantelling | Mantelling
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16284824297863-52e9496edf474f5c8|16287597150680-07e049face434fbfb', '16284824297863-52e9496edf474f5c8', '16287597150680-07e049face434fbfb')

-- Helm | Helm
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16285529899501-d28f80fb18a7411cb|16287784665352-62869ae6958e4deea', '16285529899501-d28f80fb18a7411cb', '16287784665352-62869ae6958e4deea')

-- Coronet | Coronet
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16285827830074-ad202fb38c614f689|16287823428798-e87b224c697e4e878', '16285827830074-ad202fb38c614f689', '16287823428798-e87b224c697e4e878')

-- LeftSupporter | Supporter
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16286103410954-06c1e844492a4438a|16287860489062-1944ba15018e4787b', '16286103410954-06c1e844492a4438a', '16287860489062-1944ba15018e4787b')

-- RightSupporter | Supporter
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16286326311881-9b53368169a34e299|16287860489062-1944ba15018e4787b', '16286326311881-9b53368169a34e299', '16287860489062-1944ba15018e4787b')

-- Motto | Motto
insert into DesignRegion_CompatibilityTag(Id, DesignRegionId, CompatibilityTagId)
values ('16286544643326-dbe98aecd80b4a348|16287889574439-1a0f21d3bf784920a', '16286544643326-dbe98aecd80b4a348', '16287889574439-1a0f21d3bf784920a')

-- Kuler - sandy stone beach ocean diver
insert into Palettes(Id, Name)
values ('16299015013094-5c5eb130afac4702b', 'Kuler - sandy stone beach ocean diver')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('16299528512142-1b9891c0b7f54f4ca', 1, '16299015013094-5c5eb130afac4702b', '#E6E2AF')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('16299981008008-60c93488c10b4d8f8', 1, '16299015013094-5c5eb130afac4702b', '#A7A37E')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('16300158507824-53241c6f467a48e6a', 1, '16299015013094-5c5eb130afac4702b', '#EFECCA')

insert into Strokes(Id, PaletteId, Color, Width)
values ('16300705545315-0647a7c0f57d469f9', '16299015013094-5c5eb130afac4702b', '#002F2F', 3)

insert into Strokes(Id, PaletteId, Color, Width)
values ('16300961050852-addaf2b8800641fbb', '16299015013094-5c5eb130afac4702b', '#046380', 3)


-- Kuler - unlike
insert into Palettes(Id, Name)
values ('17356785688033-9e07eca871544023a', 'Kuler - Unlike')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17358222366265-24f0c5de692e473d8', 1, '17356785688033-9e07eca871544023a', '#88A825')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17356964620568-ac09690fdc494667a', 1, '17356785688033-9e07eca871544023a', '#35203B')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17357002781865-cfe7955c55d049d99', 1, '17356785688033-9e07eca871544023a', '#911146')

insert into Strokes(Id, PaletteId, Color, Width)
values ('17357035047828-d1521e76cab048158', '17356785688033-9e07eca871544023a', '#ED8C2B', 3)

insert into Strokes(Id, PaletteId, Color, Width)
values ('17357083173350-3b39df80b7d24a6ab', '17356785688033-9e07eca871544023a', '#CF4A30', 3)

-- Kuler - Avoidance
insert into Palettes(Id, Name)
values ('17420954799731-e18a9862f86b46f8b', 'Kuler - Unlike')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17421153295968-e9c3d2623fbe43058', 1, '17420954799731-e18a9862f86b46f8b', '#5E0042')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17421183921350-269845b694794f829', 1, '17420954799731-e18a9862f86b46f8b', '#2C2233')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17421213270184-37cc554d36bc4e4da', 1, '17420954799731-e18a9862f86b46f8b', '#005869')

insert into Strokes(Id, PaletteId, Color, Width)
values ('17421242042963-f4ada1babd2543d7a', '17420954799731-e18a9862f86b46f8b', '#00856A', 3)

insert into Strokes(Id, PaletteId, Color, Width)
values ('17421271418307-03693822f0e84dd2b', '17420954799731-e18a9862f86b46f8b', '#8DB500', 3)

-- Kuler - Avoidance
insert into Palettes(Id, Name)
values ('17422281803177-3bbd1744cb3c42cea', 'Kuler - Unlike')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17422424235869-70e7e6eae62242c79', 1, '17422281803177-3bbd1744cb3c42cea', '#F2F2F2')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17422463839467-fadb26a9496d49219', 1, '17422281803177-3bbd1744cb3c42cea', '#C6E070')

insert into Fills(Id, FillType, PaletteId, SolidColorFill_Color)
values ('17422498266592-ff478c241f1c48279', 1, '17422281803177-3bbd1744cb3c42cea', '#91C46C')

insert into Strokes(Id, PaletteId, Color, Width)
values ('17422536079554-27b1c761270a4fbca', '17422281803177-3bbd1744cb3c42cea', '#287D7D', 3)

insert into Strokes(Id, PaletteId, Color, Width)
values ('17422566915641-6f330e34b9a54bc7b', '17422281803177-3bbd1744cb3c42cea', '#1C344C', 3)

-- MARKUP FRAGMENTS
insert into MarkupFragments(Id, Content)
values ('16302105072040-550b02153b5c4f53b', '<path data-stroke-index="0" data-fill-index="0" stroke="black" fill="white" d="m 128.456,39.107671 c 9.047,0.565 11.874,4.52301 11.874,4.52301 -10.366,5.278 -11.874,-4.52301 -11.874,-4.52301 m 6.933,33.481 c 0.808,-0.222 1.462,-0.56499 2.111,-0.55399 4.42,0.074 8.84,0.21799 13.26,0.35199 0.837,0.025 1.359,0.481 1.122,1.32401 -0.478,1.699 -1.035,3.379 -1.647,5.034 -0.106,0.284 -0.631,0.543 -0.988,0.58399 -4.046,0.46301 -7.839,-0.22899 -11.175,-2.687 -1.304,-0.961 -2.301,-2.18899 -2.683,-4.053 m 15.657,-8.08999 c -0.659,0.159 -1.352,0.20499 -2.034,0.237 -4.089,0.191 -8.178,0.364 -12.064,0.534 0,-2.045 -0.051,-3.708 0.041,-5.362 0.02,-0.366 0.531,-0.981 0.848,-1.00201 1.112,-0.075 2.255,0.20601 3.358,0.088 3.893,-0.41501 7.292,0.747 10.307,3.108 0.439,0.344 0.608,1.144 0.689,1.76199 0.022,0.16601 -0.715,0.53101 -1.145,0.63501 M 3.987,32.162671 c -1.313,-0.683 -2.455,-1.696 -3.987,-2.779 0.494,1.528 0.888,2.865 1.36,4.174 1.814,5.03401 6.637,8.511 9.196,13.095 0.712,1.27601 1.249,2.637 1.497,4.142 0.971,5.901 -2.83,10.91 -8.108,13.14301 -0.196,0.083 -0.383,0.18999 -0.811,0.404 0.674,0.38 1.194,0.62299 1.658,0.947 0.725,0.50599 1.457,1.01799 2.106,1.615 0.936,0.86199 3.379,0.98999 4.907,-0.12301 2.086,-1.51999 4.09,-3.178 5.967,-4.94799 1.347,-1.26901 2.542,-1.46101 3.774,0.028 0.833,1.00799 1.566,2.15099 2.106,3.342 1.718,3.78499 2.643,7.759 2.239,11.94099 -0.103,1.065 -0.533,2.098 -0.881,3.39901 4.516,-0.24 6.635,-3.13101 8.308,-6.81901 1.836,0.88201 2.75,2.261 2.746,4.677 1.407,-0.975 2.698,-1.662 3.734,-2.624 4.229,-3.923 4.74,-8.77 3.085,-13.98399 -0.631,-1.99 -1.615,-3.87101 -2.472,-5.78601 -0.444,-0.99199 -5.128,-7.97399 0.497,-3.971 0.546,0.495 1.72,1.33501 2.682,2.207 0.488,-2.22299 0.523,-4.407 0.756,-6.359 0.635,-5.322 -0.706,-10.339 -2.667,-15.227 -0.341,-0.848 -0.663,-1.709 -1.082,-2.519 -1.09,-2.112 -0.838,-4.143 0.607,-5.893 1.308,-1.584 3.302,-1.72 5.164,-1.477 3.414,0.444 5.999,2.539 8.477,4.732 2.856,2.529 5.378,5.38 7.334,8.645 2.567,4.28401 4.318,8.876 5.322,13.828 0.794,3.918 0.873,7.84401 0.523,11.699 -0.504,5.562 -1.964,10.95901 -5.124,15.744 -3.702,5.606 -8.353,10.39201 -13,15.18 -8.562,8.820999 -17.139,17.610999 -24.851,27.225999 -6.487,8.089 -9.817,17.23801 -11.02,27.43301 -1.124,9.53 -0.45,18.864 2.012,28.082 1.595,5.968 3.854,11.644 7.243,16.908 4.334,6.73 10.219,11.26 17.815,13.478 8.109,2.368 16.399,2.676 24.685,1.069 3.292,-0.638 6.526,-1.686 9.7,-2.796 1.511,-0.529 2.867,-0.464 3.918,0.129 -0.549,-6.019 -0.873,-16.339 1.752,-26.209 -2.4,4.009 -5.256,7.681 -8.852,10.761 -10.095,8.648 -22.296,9.05 -32.905,0.995 -6.498,-4.934 -10.937,-11.274 -12.192,-19.485 -1.002,-6.558 1.028,-12.355 6.098,-16.466 4.443,-3.603 9.843,-4.828 15.421,-2.36 1.25,0.552 2.371,1.722 3.173,2.871 0.853,1.222 1.017,2.808 -0.08,4.086 -1.447,1.688 -3.016,3.27 -4.475,4.836 0.845,0.947 1.733,2.055 2.741,3.039 1.098,1.072 3.808,1.031 5.693,-0.213 1.776,-1.173 3.425,-2.546 5.069,-3.905 0.829,-0.685 2.329,-0.865 3.201,0.07 0.975,1.045 1.998,2.22 2.466,3.529 1.315,3.682 1.612,7.483 0.408,11.299 -0.16,0.505 -0.248,1.033 -0.094,1.755 0.83,-0.67 1.688,-1.308 2.485,-2.016 5.698,-5.057 7.059,-12.118 3.585,-18.896 -0.809,-1.58 -1.8,-3.075 -2.797,-4.548 -0.551,-0.815 -0.542,-1.392 0.284,-1.943 0.871,-0.581 1.736,-1.172 2.593,-1.774 3.907,-2.744 5.202,-6.542 4.476,-11.146 -0.026,-0.161 -0.136,-0.309 -0.216,-0.485 -5.079,5.255 -9.279,7.708 -13.718,7.897 -0.456,-4.244 -3.615,-6.217 -7.216,-7.877 0.464,-1.104 1.031,-2.066 1.272,-3.104 0.572,-2.472 1.418,-4.986 1.379,-7.47201 -0.078,-4.977 -3.174,-8.249 -8.689,-9.804 0.277,0.667 0.521,1.22 0.739,1.782 0.841,2.172 1.767,4.301 0.856,6.743 -0.286,0.765 -0.082,1.72 -0.073,2.58701 0.033,3.487 -2.246,6.376 -5.658,6.973 -7.379,1.292 -13.326,4.837 -17.661,10.997 -0.174,0.248 -0.399,0.46 -0.898,0.626 0.076,-0.965 0.07,-1.945 0.242,-2.893 2.257,-12.453 7.794,-23.28001 16.658,-32.26601 3.703,-3.755 7.936,-6.998 12,-10.387 10.876,-9.068999 17.954,-19.732999 20.834,-32.119989 -0.428,7.23699 6.976,11.153 12.72,13.09399 -4.844,9.292 -12.51,6.093 -12.51,6.093 2.154,6.25 9.263,7.461 15.089,7.321 0.012,0.531 0.024,1.062 0.037,1.59301 -4.07,17.173989 -14.852,12.674989 -14.852,12.674989 2.428,7.042 11.147,7.687 17.232,7.209 0.538,3.62 0.916,6.671 0.715,7.239 -0.841,2.38 -2.153,3.731 -5.595,5.69401 5.866,4.02 13.092,0.243 14.591,-2.53201 0.085,-0.156 0.214,-0.299 0.353,-0.412 0.097,-0.079 0.244,-0.096 0.362,-0.138 1.299,1.65301 1.352,5.64101 0.086,7.63501 -3.184,5.014 -6.622,9.885 -9.519,15.059 -3.535,6.314 -6.847,12.752 -10.203,19.167 l 0.224,0.118 c -6.85,12.972 -5.674,30.184 -4.874,36.859 0.237,0.622 0.39,1.341 0.432,2.167 0.166,3.237 -0.013,6.489 0.053,9.733 0.092,4.414 -2.899,6.368 -6.342,7.706 -4.505,1.751 -8.921,0.754 -13.141,-1.216 -4.666,-2.179 -8.333,-5.56 -11.328,-9.708 -0.153,-0.213 -0.323,-0.413 -0.682,-0.869 0.115,1.224 0.251,2.14 0.275,3.06 0.029,1.093 0.043,2.198 -0.075,3.282 -0.451,4.108 -2.59,6.312 -6.673,6.969 -0.474,0.076 -0.949,0.156 -1.577,0.259 0.51,0.608 0.938,1.061 1.3,1.563 0.607,0.843 0.916,1.679 0.438,2.787 -0.303,0.704 -0.307,1.629 -0.176,2.41 0.284,1.698 -0.211,3.016 -1.447,4.173 -1.826,1.711 -3.993,2.703 -6.404,3.225 -0.667,0.144 -1.344,0.243 -2.242,0.403 0.235,0.497 0.381,0.951 0.639,1.329 0.831,1.221 1.553,2.576 2.612,3.561 0.733,0.682 1.958,0.83 2.957,1.233 1.106,0.447 2.205,0.912 3.661,1.516 -2.054,1.754 -4.213,1.73 -6.581,2.297 0.796,0.857 1.378,1.567 2.048,2.183 0.642,0.59 1.258,1.055 1.163,2.132 -0.041,0.465 0.561,1.115 1.031,1.471 1.192,0.902 2.559,1.585 3.696,2.545 1.129,0.954 1.102,1.475 -0.145,2.196 -1.956,1.132 -4.029,2.063 -6.65,3.455 0.394,0.139 0.821,0.221 1.175,0.428 1.229,0.716 2.242,1.531 2.705,3.06 0.203,0.668 1.372,1.075 2.144,1.529 1.113,0.654 2.264,1.243 3.398,1.859 -0.014,0.18 -0.029,0.36 -0.043,0.54 -1.717,0.493 -3.433,0.986 -5.53,1.588 0.796,0.805 1.488,1.507 2.182,2.207 0.342,0.346 0.932,0.655 0.987,1.041 0.295,2.077 1.932,2.45 3.499,2.91 1.709,0.502 3.458,0.876 5.144,1.443 0.634,0.213 1.138,0.813 1.702,1.236 -0.018,0.181 -0.036,0.361 -0.054,0.542 -2.44,1.279 -4.88,2.558 -7.428,3.895 0.45,0.329 0.911,0.675 1.381,1.009 0.817,0.581 1.717,0.904 2.051,2.164 0.177,0.664 1.554,1.195 2.48,1.429 1.45,0.367 2.978,0.427 4.472,0.624 3.632,0.478 4.515,3.1 4.434,6.287 -0.022,0.854 -1.127,1.797 -1.923,2.48 -0.708,0.608 -1.681,0.903 -2.528,1.351 -2.301,1.218 -3.032,3.187 -2.535,5.742 0.509,2.613 0.705,5.285 1.064,7.928 0.304,2.246 0.405,4.519 3.183,5.47 1.215,0.417 2.187,1.543 3.432,2.468 -0.088,-1.716 -0.25,-3.156 -0.216,-4.591 0.065,-2.676 0.626,-3.319 3.174,-4.007 2.805,-0.758 4.885,-3.886 4.498,-6.772 -0.151,-1.128 -0.354,-2.252 -0.459,-3.384 -0.089,-0.965 0.461,-1.554 1.392,-1.39 0.952,0.168 2.036,0.386 2.751,0.966 3.817,3.095 5.608,7.252 5.741,12.097 0.07,2.562 0.729,4.59 2.938,6.226 2.812,2.08 3.816,5.198 3.834,8.639 10e-4,0.137 -0.018,0.275 -0.006,0.41 0.008,0.086 0.056,0.17 0.229,0.651 1.213,-1.121 2.446,-2.033 3.393,-3.181 1.874,-2.27 2.567,-4.995 2.6,-7.916 0.021,-1.853 0.78,-2.261 2.363,-1.315 1.184,0.707 2.452,1.32 3.497,2.196 0.517,0.434 0.616,1.368 0.952,2.191 3.848,-3.255 4.189,-7.489 1.116,-12.752 1.083,-0.885 1.119,-0.905 0.397,-2.161 -0.856,-1.49 -1.782,-2.94 -2.758,-4.54 1.564,-0.507 2.944,-0.141 4.352,0.229 0.576,0.151 1.444,0.294 1.795,-0.007 0.97,-0.832 1.563,-0.227 2.218,0.305 0.762,0.619 1.486,1.287 2.421,2.104 0.358,-3.503 -0.439,-6.425 -2.485,-9.002 -2.031,-2.559 -4.846,-3.949 -7.763,-5.059 -0.029,-0.189 -0.066,-0.282 -0.052,-0.366 0.526,-3.321 -0.276,-4.157 -3.56,-3.539 -2.286,0.43 -4.528,1.112 -6.823,1.48 -3.022,0.485 -6.466,-1.589 -7.606,-4.429 -0.544,-1.356 -0.88,-2.81 -1.549,-4.096 -0.769,-1.48 -1.537,-3.14 -2.767,-4.169 -3.918,-3.278 -4.845,-7.399 -3.87,-12.101 0.535,-2.579 1.425,-5.084 2.169,-7.619 0.385,-1.314 1.312,-1.379 2.215,-0.695 1.156,0.877 2.225,1.889 3.233,2.938 1.99,2.073 4.125,3.894 7.021,4.54 2.962,0.66 5.688,0.072 8.239,-1.552 0.984,-0.625 2.011,-1.251 3.106,-1.618 1.659,-0.558 3.042,0.223 3.493,1.894 0.437,1.62 0.431,3.201 -0.556,4.728 -1.316,2.037 -3.164,3.386 -5.243,4.496 -0.439,0.234 -0.893,0.44 -1.478,0.727 1.721,1.57 3.598,1.988 5.66,1.452 1.004,-0.261 1.997,-0.678 2.902,-1.189 3.396,-1.917 6.134,0.766 6.237,3.67 0.038,1.08 -0.104,2.167 -0.172,3.412 1.413,-0.335 2.344,-1.192 2.837,-2.296 0.962,-2.157 1.798,-4.381 2.515,-6.632 0.335,-1.052 0.701,-1.678 1.902,-1.713 0.535,-0.015 1.059,-0.426 1.779,-0.74 0.423,1.072 0.897,2.143 1.267,3.248 0.359,1.073 0.611,2.181 0.977,3.521 0.245,-0.485 0.41,-0.736 0.506,-1.011 2.308,-6.612 0.045,-13.797 -5.91,-18.265 -1.739,-1.305 -3.702,-2.339 -5.65,-3.326 -0.905,-0.459 -1.313,-0.974 -1.046,-1.891 0.977,-3.366 1.758,-6.817 3.064,-10.053 1.691,-4.192 3.714,-8.248 5.604,-12.36 0.982,-3.283 0.703,-4.947 0.423,-6.562 -0.14,-0.808 -0.285,-1.644 -0.285,-2.652 0,-1.507 0.503,-2.017 1.064,-2.446 0.426,-0.327 1.07,-0.821 -1.034,-8.186 -1.313,-4.595 -4.427,-7.331 -9.257,-8.132 -3.703,-0.614 -7.076,0.184 -7.11,0.193 -0.41,0.099 -0.821,-0.152 -0.92,-0.561 -0.099,-0.409 0.151,-0.821 0.56,-0.921 0.146,-0.035 14.644,-3.418 18.193,9.002 2.274,7.959 1.669,8.915 0.495,9.815 -0.291,0.223 -0.466,0.357 -0.466,1.236 0,0.877 0.127,1.613 0.262,2.392 0.2,1.153 0.398,2.331 0.232,3.972 0.372,-0.128 0.775,-0.193 1.186,-0.237 10.655,-1.157 17.938,3.926 22.132,13.18 1.471,3.245 1.865,6.672 1.836,10.157 -0.035,4.098 -3.726,8.066 -8.177,8.988 -3.081,0.639 -6.09,0.227 -9.076,-0.581 -0.571,-0.154 -1.14,-0.317 -2.086,-0.279 0.719,1.464 1.463,2.917 2.144,4.398 0.248,0.54 0.433,1.14 0.493,1.729 0.112,1.099 1.209,2.384 2.83,2.624 1.836,0.272 3.742,0.406 5.583,0.234 2.806,-0.262 4.973,0.553 6.717,2.779 1.03,1.314 2.113,2.671 4.151,2.469 0.436,-0.043 0.929,0.488 1.487,0.806 0.767,-1.474 1.534,-2.908 2.265,-4.36 0.574,-1.141 1.471,-1.363 2.288,-0.49 1.036,1.108 1.879,2.408 2.736,3.672 0.808,1.191 1.54,2.434 2.274,3.672 0.268,0.452 0.447,0.956 0.779,1.684 0.699,-0.59 1.501,-1.054 2.012,-1.737 1.669,-2.227 3.262,-4.513 4.828,-6.815 1.413,-2.077 2.873,-2.164 4.463,-0.131 1.029,1.316 1.981,2.692 3.134,4.27 1.169,-0.921 2.419,-1.643 3.317,-2.671 1.646,-1.885 3.114,-3.929 4.6,-5.948 0.953,-1.293 1.783,-1.614 3.072,-0.624 1.6,1.229 3.027,2.681 4.784,4.268 0.703,-0.722 1.81,-1.481 2.389,-2.531 1.185,-2.148 2.096,-4.449 3.1,-6.695 0.445,-0.996 0.705,-2.132 2.102,-2.231 1.535,-0.11 3.075,0.057 3.858,1.509 0.832,1.544 1.49,3.222 1.956,4.916 0.891,3.232 2.775,5.48 5.879,6.738 1.739,0.705 3.093,1.863 4.143,3.447 0.481,0.726 1.326,1.2 1.881,1.889 0.405,0.503 0.663,1.151 0.879,1.772 0.169,0.489 0.174,1.034 0.309,1.928 4.824,-5.545 4.298,-11.061 1.118,-16.838 0.906,-0.09 1.644,-0.163 2.649,-0.264 -1.078,-2.272 -2.397,-4.008 -4.581,-4.846 -1.266,-0.485 -2.672,-0.594 -3.962,-1.032 -0.828,-0.281 -1.695,-0.715 -2.296,-1.326 -0.79,-0.802 -0.486,-1.394 0.651,-1.458 3.807,-0.212 7.52,0.337 10.911,2.085 2.202,1.135 4.33,0.684 6.5,0.33 0.487,-0.079 1.054,-0.646 1.281,-1.131 0.495,-1.054 1.275,-1.363 2.323,-1.278 0.727,0.059 1.451,0.197 2.178,0.212 0.625,0.014 1.523,0.148 1.831,-0.194 1.035,-1.147 1.885,-0.458 2.766,0.05 0.86,0.496 1.669,1.08 2.691,1.75 0.001,-2.435 -0.683,-4.501 -1.915,-6.387 -1.924,-2.942 -4.801,-4.535 -8.057,-5.59 -0.498,-0.161 -0.909,-0.588 -1.361,-0.891 0.375,-0.401 0.741,-0.811 1.131,-1.198 0.125,-0.124 0.371,-0.146 0.462,-0.281 1.346,-2.007 3.045,-1.199 4.72,-0.614 0.412,0.143 0.827,0.275 1.29,0.428 -0.695,-4.371 -3.272,-8.09 -9.264,-8.785 0.017,-1.698 -0.071,-1.781 -1.872,-1.614 -0.545,0.05 -1.086,0.149 -1.627,0.233 -0.482,0.074 -0.962,0.157 -1.724,0.283 2.656,-3.56 5.994,-5.071 10.184,-4.945 -2.832,-3.903 -9.291,-6.168 -17.175,-1.678 -2.036,-1.322 -2.94,-1.098 -3.801,1.201 -0.767,2.049 -1.343,4.171 -1.961,6.273 -0.467,1.586 -1.288,2.877 -2.622,3.901 -1.485,1.139 -2.863,2.417 -4.304,3.614 -2.145,1.781 -4.637,2.801 -7.402,2.918 -10.181,0.429 -20.365,0.784 -30.548,1.159 -0.398,0.015 -0.8,-0.059 -1.568,-0.122 0.328,-1.702 0.454,-3.345 0.977,-4.851 0.853,-2.455 2.095,-4.776 2.931,-7.235 0.861,-2.534 1.402,-5.176 2.159,-7.748 0.135,-0.457 0.571,-1.007 0.993,-1.156 0.661,-0.232 1.433,-0.128 2.151,-0.222 0.98,-0.128 2.133,-0.033 2.89,-0.527 1.386,-0.905 2.72,-0.874 4.18,-0.534 0.466,0.109 0.943,0.177 1.592,0.296 -0.431,-1.141 -0.78,-2.063 -1.296,-3.431 0.992,0.447 1.628,0.691 2.225,1.009 1.368,0.729 2.693,1.544 4.085,2.224 0.799,0.389 1.668,0.739 2.515,-0.105 0.198,-0.198 0.764,-0.075 1.152,-0.026 3.539,0.452 7.076,-0.38 10.494,-2.632 -3.274,0.125 -6.192,-0.09 -8.843,-1.532 -2.643,-1.437 -4.628,-3.5 -6.004,-6.555 10.33,-2.298 16.358,-8.567 17.886,-19.392 -4.734,6.251 -10.649,9.731 -18.523,10.302 0.414,-0.792 0.692,-1.34 0.984,-1.881 2.78,-5.151 2.247,-9.313 -1.89,-13.445 -1.475,-1.473 -3.186,-2.711 -4.789,-4.056 -0.093,0.069 -0.187,0.139 -0.28,0.208 0.351,0.786 0.722,1.564 1.051,2.359 1.006,2.436 1.715,4.898 1.119,7.595 -0.495,2.235 -1.561,4.089 -3.653,5.007 -2.031,0.892 -4.178,1.609 -6.344,2.076 -1.971,0.425 -3.784,-0.54 -5.403,-1.587 -2.069,-1.338 -4.011,-2.872 -6.045,-4.265 -2.635,-1.806 -5.233,-3.683 -7.983,-5.298 -3.499,-2.057 -7.324,-2.174 -11.146,-1.052 -1.232,0.362 -2.316,1.219 -3.48,1.823 -1.479,0.769 -2.897,1.859 -4.473,2.199 -4.53,0.979 -9.143,0.828 -13.904,0.474 0.258,-3.662 1.002,-7.099 3.211,-9.839 0.791,-0.981 2.781,-1.098 4.267,-1.394 3.033,-0.603 6.173,-0.791 9.128,-1.638 5.083,-1.457 9.087,-4.79 12.76,-8.441 1.968,-1.956 3.653,-4.204 5.39,-6.381 0.873,-1.093 1.892,-1.364 2.973,-0.625 1.304,0.892 2.456,2.006 3.69,3.004 0.35,0.283 0.789,0.747 1.129,0.701 1.761,-0.239 3.022,0.658 4.372,1.954 0.052,-0.947 0.084,-1.602 0.123,-2.257 0.071,-1.176 0.732,-1.576 1.753,-1.114 0.856,0.387 1.716,0.875 2.407,1.502 2.094,1.902 3.078,2.082 5.671,1.034 1.031,-0.417 2.422,-0.707 3.525,-0.827 0.57,-0.061 1.742,-0.721 2.342,-0.509 -0.233,0.483 -1.642,2.78 -1.809,3.102 1.853,0.107 3.537,-0.1 5.066,-0.913 2.776,-1.477 4.758,0.138 5.861,2.391 0.272,0.554 0.124,1.315 0.189,2.279 0.838,-0.462 1.591,-0.896 2.362,-1.295 0.475,-0.247 3.955,-0.513 2.874,1.559 -0.415,0.795 -0.054,2.388 -0.571,3.305 2.006,-0.11 8.845,-8.007 9.697,-7.012 0.775,0.907 2.184,5.147 2.964,6.392 0.25,-0.139 2.684,-4.368 3.07,-4.197 1.151,0.509 2.666,3.32 3.199,4.816 0.201,0.565 0.566,1.072 0.895,1.676 1.105,-1.477 0.764,-4.329 2.112,-3.175 1.319,1.13 0.413,7.098 1.656,8.385 0.058,-0.161 2.781,-2.425 3.087,-3.28 1.092,1.498 2.051,2.767 2.958,4.073 0.831,1.196 1.512,2.509 2.445,3.612 0.434,0.514 3.267,1.538 3.835,1.931 0.581,0.402 1.069,0.938 1.891,1.68 0.084,-1.344 0.196,-2.321 0.197,-3.298 0.003,-3.29 -0.088,-6.582 -0.016,-9.87 0.019,-0.88 0.496,-2.495 0.758,-2.494 1.103,0.004 2.372,0.244 3.27,0.854 1.941,1.317 3.247,3.279 3.729,5.571 0.701,3.336 1.144,6.726 1.73,10.087 0.155,0.892 0.436,1.761 0.648,2.644 0.477,1.981 2.547,2.374 3.671,3.702 0.697,0.823 1.471,1.643 1.921,2.601 0.866,1.844 2.434,3.49 1.688,5.897 4.642,-1.883 6.412,-8.169 4.566,-15.449 1.424,-0.954 1.584,-1.427 0.55,-2.882 -0.842,-1.184 -1.869,-2.242 -2.863,-3.31 -1.69,-1.818 -3.317,-3.67 -4.041,-6.118 -0.561,-1.898 -0.216,-2.424 1.672,-1.823 1.787,0.568 3.526,1.517 5.05,2.623 1.686,1.225 3.141,2.782 4.631,4.26 1.796,1.781 3.808,3.153 6.338,3.585 2.103,0.359 3.545,1.793 5.17,2.961 0.51,0.367 1.221,0.443 1.816,0.707 0.375,0.167 0.84,0.355 1.029,0.674 0.46,0.776 0.782,1.633 1.165,2.47 3.107,-2.947 2.335,-8.977 -1.933,-15.392 1.747,-1.906 1.585,-3.252 -0.883,-4.216 -3.053,-1.192 -6.245,-2.028 -9.373,-3.031 -1.774,-0.569 -3.54,-1.164 -5.627,-1.852 1.487,-0.394 2.669,-0.578 3.745,-1.021 1.76,-0.723 3.583,-1.424 5.124,-2.501 1.068,-0.746 2.155,-1.022 3.32,-1.352 0.734,-0.207 1.376,-0.726 2.086,-1.045 0.336,-0.152 0.771,-0.3 1.101,-0.215 1.571,0.404 3.121,0.888 4.83,1.388 -1.829,-5.949 -6.074,-8.627 -11.914,-9.201 -0.316,-2.26 -1.742,-2.793 -3.679,-2.137 -1.149,0.389 -2.23,1.05 -3.255,1.721 -1.373,0.901 -2.739,1.844 -3.974,2.921 -3.701,3.228 -7.928,3.584 -12.486,2.542 -4.65,-1.063 -8.86,-2.992 -12.776,-5.754 -10.758,-7.587 -21.578,-15.088 -32.382,-22.61 0,0 -0.001,0 -0.001,0 -0.138,0 -0.279,-0.038 -0.405,-0.117 l -22.855,-14.36201 c -3.258,-2.047 -6.054,-4.666 -8.311,-7.782 l -2.445,-3.377 c -0.247,-0.341 -0.171,-0.818 0.17,-1.065 0.341,-0.246 0.817,-0.17 1.065,0.171 l 2.445,3.377 c 2.142,2.957 4.796,5.442 7.888,7.385 l 15.734,9.88801 c 0.262,-0.322 0.513,-0.62801 0.769,-0.92501 3.042,-3.535 5.331,-7.486 7.003,-11.856 0.446,-1.163 1.802,-1.977 2.974,-3.195 0.938,1.773 1.671,3.158 2.404,4.544 0.09,0.002 0.18,0.004 0.27,0.006 0.157,-1.28 1.807,-13.504 2.61,-12.279 0.82,1.251 1.597,2.531 2.576,4.092 0.733,-4.57 -0.743,-8.575 -1.437,-12.723999 0.285,-0.113 0.524,-0.215 0.767,-0.305 2.258,-0.832 2.367,-0.799 3.598,1.348 0.423,0.738 0.791,1.508 1.48,2.249 0.077,-0.641 -0.406,-8.833 -0.697,-12.29 -0.112,-1.334 -0.534,-2.741 0.807,-4.053 0.845,0.987 1.675,1.955 2.741,3.199 0.081,-1.051 -0.261,-9.568 -0.396,-13.081 -0.021,-0.569 0.293,-1.405 0.73,-1.692 1.88,-1.229 2.166,-1.062 2.648,1.126 0.064,0.293 0.145,0.582 0.513,0.942 0.357,-0.955 4.775,-15.411 5.128,-16.202 1.726,-3.874 8.027,-5.242 11.085,-1.984 2.913,3.102 6.304,3.898 10.427,3.314 5.637,-0.798 9.825,-3.531 12.754,-8.333 0.094,-0.155 0.171,-0.324 0.236,-0.494 0.03,-0.079 0.013,-0.176 0.026,-0.427 -3.965,1.268 -7.913,1.905 -11.83,0.052 0.146,-2.046 -1.606,-4.152 -3.936,-4.685 -0.931,-0.214 -1.895,-0.292 -2.849,-0.386 -1.996,-0.196 -3.556,-1.174 -4.687,-2.761 -0.281,-0.393 -0.326,-1.226 -0.101,-1.662 1.264,-2.45 3.409,-3.36 6.047,-2.92 2.439,0.407 4.404,-0.902 6.54,-1.575 0.433,-0.136 0.856,-0.74 1.015,-1.218 0.352,-1.061 1.11,-1.363 2.083,-1.455 0.909,-0.086 1.823,-0.154 2.722,-0.307 0.479,-0.082 1.153,-0.189 1.358,-0.521 0.881,-1.427 2.1,-0.888 3.217,-0.638 1.137,0.255 2.232,0.7 3.552,1.128 -0.812,-2.49 -2.063,-4.489 -4,-6.034 -2.585,-2.062 -5.616,-2.736 -8.842,-2.847 -0.448,-0.015 -1.117,-0.253 -1.287,-0.589 -0.872,-1.726 -1.206,-1.868 -3.029,-0.921 -1.374,0.714 -1.374,0.714 -2.359,-0.342 0.992,-2.778 3.777,-4.575 9.308,-6.004 -1.694,-2.822 -6.241,-4.505 -9.729,-3.73 -2.276,0.506 -4.146,1.917 -6.42,2.557 -1.958,0.551 -3.5,2.583 -5.183,3.927 -0.151,-1.001 -1.136,-1.381 -2.48,-1.309 -0.419,-4.603 1.502,-8.2229998 4.306,-11.5599998 -4.301,-0.456 -8.98,2.393 -11.218,6.7629998 -0.333,0.649 -0.584,1.34 -0.9,1.999 -0.642,1.339 -1.332,2.656 -1.944,4.008 -0.316,0.7 -0.631,1.44 -0.732,2.191 -0.425,3.153 -0.229,6.271 0.663,9.344 1.406,4.85 0.747,6.818 -3.548,9.765 -5.606,3.848 -10.718,8.239 -15.267,13.291 -4.696,5.217 -8.345,11.026 -10.749,17.678 -4.704,13.017 -13.285,22.772 -25.481,29.312999 -0.493,0.265 -1.333,0.171 -1.904,-0.043 -3.806,-1.427999 -7.567,-2.978999 -11.375,-4.403999 -3.356,-1.256 -6.764,-2.132 -10.444,-1.722 -1.447,0.161 -3.021,-0.149 -4.441,-0.57 -2.447,-0.724 -3.693,-2.594 -3.767,-5.141 -0.05,-1.704 0.381,-2.209 2.075,-2.046 2.587,0.25 5.154,0.72 7.743,0.936 3.501,0.291 7.014,0.461 10.526,0.605 0.594,0.024 1.375,-0.166 1.776,-0.558 1.074,-1.052 2.25,-1.015 3.552,-0.737 1.018,0.217 2.05,0.367 3.137,0.557 -3.464,-3.991 -3.786,-4.917 -3.067,-10.731 -0.848,0.41 -1.647,0.798 -2.592,1.255 -0.423,-1.66 -0.847,-3.178 -1.185,-4.716 -0.035,-0.157 -0.04,-0.284 -0.025,-0.391 3.403,1.262 11.607,3.092 21.57,-3.185 l -4.283,-1.146 c 3.09,-2.781 4.961,-6.907 4.983,-11.392 0.011,-2.498 -0.231,-4.938 -1.418,-7.186 -0.882,-1.667 -2.107,-2.935 -4.285,-3.335 0,2.553 0.165,4.974 -0.039,7.364 -0.167,1.95 -0.84,3.605 -1.969,4.958 2.358,-3.42 0.144,-8.432 -3.503,-6.609 -3.657,1.829 -0.96,3.658 -0.96,3.658 -1.142,-1.28 2.057,-3.109 2.24,1.645 0.087,2.258 -0.921,3.546 -2.002,4.278 -3.13,1.399 -6.494,2.002 -10.204,2.423 1.092,-2.06 1.017,-4.394 2.848,-5.799 0.836,-0.64 1.868,-1.056 2.859,-1.451 0.811,-0.324 0.998,-0.716 0.959,-1.628 -0.142,-3.321 -0.231,-6.656 -0.083,-9.974 0.063,-1.412 0.663,-2.836 1.197,-4.184 0.722,-1.822 -0.358,-2.872 -1.852,-3.237 -1.005,-0.246 -2.087,-0.249 -3.133,-0.237 -4.749,0.056 -9.153,-1.114 -13.109,-3.749 -2.549,-1.697 -5.242,-2.8 -8.067,-3.413 l 0.24,-0.165 c -0.381,0.005 -0.748,10e-4 -1.109,-0.006 -1.419,-0.256 -2.871,-0.391 -4.353,-0.421 -10.066,-1.904 -8.657,-9.505 -8.657,-9.505 -3.598,2.176 -4.312,6.18 -3.947,10.059 -11.277,-2.702 -8.863,-10.736 -8.863,-10.736 -7.121,3.313 -4.933,14.056 -3.743,18.302 -12.886,2.471 -14.411,-7.078 -14.411,-7.078 -5.652,6.918 1.279,16.691 4.962,21.013 -9.494,3.613 -13.147,-3.648 -13.147,-3.648 -3.209,8.098 5.808,15.161 10.726,18.292 -9.024,9.748 -15.892,3.02 -16.025,2.888 1.478,-6.369 1.85,-13.193 1.052,-20.491 -0.932,-8.521 -4.208,-16.19 -9.456,-23.048 -5.322,-6.954 -12.448,-10.221 -20.974,-10.833 -3.312,-0.238 -6.54,-0.803 -9.576,-2.16 -4.137,-1.85 -9.071,-4.5029998 -8.433,-9.2489998 1.264,-9.418 -3.712,1.314 -3.735,3.862 -0.065,7.1739998 -0.238,13.3769998 -4.988,19.2299998 -4.143,5.105 -10.854,8.571 -17.21,5.269" />')

-- SHAPES
insert into Shapes(Id, ShapeType, Name, Width, Height, NumberOfFillsSupported, NumberOfStrokesSupported, BasicShape_MarkupFragmentId)
values('16302233926171-d811a4d3f5e2438ba', 1, 'Lion', 378, 334, 1, 1, '16302105072040-550b02153b5c4f53b')

-- LICENSES
insert into Licenses(Id, LicenseName, LicenseUrl, AttributionRequired)
values('16364871084207-6ccb2d61ccfa4e80a', 'Eezy Premium', 'http://www.vecteezy.com/frequently-asked-questions', 0)

-- CONTENT LICENSES
insert into ContentLicenses(Id, LicenseId, ShapeId, ContentUrl)
values('17047177177938-364a36e8c8e14b158', '16364871084207-6ccb2d61ccfa4e80a', '16302233926171-d811a4d3f5e2438ba', 'http://www.vecteezy.com/vector-art/100575-lion-rampant-flat-silhouettes')