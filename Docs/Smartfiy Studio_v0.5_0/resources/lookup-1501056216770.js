(function(window, undefined) {
  var dictionary = {
    "624f2e2e-99f4-4899-8305-b4630f3490a8": "Solution Structure",
    "fe7c739d-e38d-4dd3-89eb-e8050138ae9f": "Toolbox",
    "d12245cc-1680-458d-89dd-4f0d7fb22724": "Main Window",
    "18518461-5e6f-49f2-962c-8534d12afc9e": "Record_task_start",
    "b8319b0d-dc4e-42a2-99bb-64aa3e7ec141": "workspace+ colors",
    "9e415898-0f31-49d7-b415-e7473fd5ce7c": "Desktop",
    "87db3cf7-6bd4-40c3-b29c-45680fb11462": "960 grid - 16 columns",
    "e5f958a4-53ae-426e-8c05-2f7d8e00b762": "960 grid - 12 columns",
    "f39803f7-df02-4169-93eb-7547fb8c961a": "Template 1",
    "8882a99c-f096-41cc-b4ae-529634b5b670": "Top Bar Icons",
    "4fefdcb0-9f17-49ca-9e90-138b3d98939c": "Main App Tabs",
    "71dab7fb-aa7f-48bc-a345-22cca2b3c95d": "Side Toolbar_DownRight",
    "e836f5bd-d384-423d-90e4-64231329e583": "neww2",
    "17e15bef-949a-4568-ac32-8a31e4ce4d81": "Footer_window",
    "2556543e-5d07-4778-ab9c-24282c35ccf5": "ComponentProperties_centre",
    "0b63b289-d0d9-4cdb-9bbb-39f1f6ca1d1f": "Main_Window_Dialogues",
    "e28f6256-18c0-472a-a86d-2258cb558b7e": "Side Toolbar_right",
    "a8b796c0-470e-47c3-be46-a614c4b21d59": "Master_1",
    "a0618400-2069-4c6b-b141-8d53a689d39e": "Main_Content window",
    "2140e15f-d921-49ce-b858-cb2140356d6c": "Side Toolbar_left",
    "0c27f18e-cb20-4ee5-bea9-2d7f4134596d": "Side Toolbar_left-del",
    "2f824c66-492d-4049-bb27-5521387ec12b": "Landing_screen",
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