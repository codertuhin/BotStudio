jQuery("#simulation")
  .on("click", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .click", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Rectangle_2")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Script_View" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Image_2")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Panel_4" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Image_4")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Panel_3" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Button_1")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Button_1": {
                      "attributes": {
                        "background-color": "#FFFFFF",
                        "background-image": "none",
                        "font-size": "12.0pt",
                        "font-family": "'Myriad Pro',Arial"
                      }
                    }
                  },{
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Button_1 .valign": {
                      "attributes": {
                        "vertical-align": "middle",
                        "text-align": "center"
                      }
                    }
                  },{
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Button_1 span": {
                      "attributes": {
                        "color": "#181818",
                        "text-align": "center",
                        "text-decoration": "none",
                        "font-family": "'Myriad Pro',Arial",
                        "font-size": "12.0pt",
                        "font-style": "normal",
                        "font-weight": "400"
                      }
                    }
                  },{
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Button_1": {
                      "attributes-ie": {
                        "-pie-background": "#FFFFFF",
                        "-pie-poll": "false"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Rectangle_5")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Recorded_Task_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("mousedown", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .mousedown", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Image_8")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-a0618400-Input_3" ],
                    "value": ""
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("pageload", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .pageload", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Recorded_Task_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Recorded_Task_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Build_Task_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_win"
                },"2" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Build_Task_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Process_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_win"
                },"3" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Process_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("focusin", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .focusin", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Input_3")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Input_3": {
                      "attributes": {
                        "background-color": "#FFFFFF",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Input_3": {
                      "attributes-ie": {
                        "-pie-background": "#FFFFFF",
                        "-pie-poll": "false"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Image_8" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("focusout", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .focusout", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Input_3")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Input_3": {
                      "attributes": {
                        "background-color": "#BFBFBF",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-a0618400-2069-4c6b-b141-8d53a689d39e #m-a0618400-Input_3": {
                      "attributes-ie": {
                        "-pie-background": "#BFBFBF",
                        "-pie-poll": "false"
                      }
                    }
                  } ],
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimHide",
                  "parameter": {
                    "target": [ "#m-a0618400-Image_8" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  })
  .on("variablechange.jim", ".m-a0618400-2069-4c6b-b141-8d53a689d39e .variablechange", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-a0618400-Build_Task_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_win"
                },"2" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Build_Task_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    } else if(jFirer.is("#m-a0618400-Process_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_win"
                },"3" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-a0618400-Process_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        }
      ];
      event.data = data;
      jEvent.launchCases(cases);
    }
  });