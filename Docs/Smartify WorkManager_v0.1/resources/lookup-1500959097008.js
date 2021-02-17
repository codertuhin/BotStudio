(function(window, undefined) {
  var dictionary = {
    "f44141b1-2a92-4b16-9d69-63d0a4e014df": "Workspace",
    "d12245cc-1680-458d-89dd-4f0d7fb22724": "Main_Window",
    "87db3cf7-6bd4-40c3-b29c-45680fb11462": "960 grid - 16 columns",
    "e5f958a4-53ae-426e-8c05-2f7d8e00b762": "960 grid - 12 columns",
    "f39803f7-df02-4169-93eb-7547fb8c961a": "Template 1",
    "76708393-08f7-4ed4-ae61-6252f1862232": "Side_bar_right_Panel",
    "d7f53983-48e4-48cb-801b-d88a5f508daa": "Side_bar_left_Panel",
    "aa0e3abb-c388-4de2-8a06-8eb74103bee7": "Main_Content_Panel",
    "bb8abf58-f55e-472d-af05-a7d1bb0cc014": "default"
  };

  var uriRE = /^(\/#)?(screens|templates|masters|scenarios)\/(.*)(\.html)?/;
  window.lookUpURL = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, url;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      url = folder + "/" + canvas;
    }
    return url;
  };

  window.lookUpName = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, canvasName;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      canvasName = dictionary[canvas];
    }
    return canvasName;
  };
})(window);