jQuery("#simulation")
  .on("click", ".m-e28f6256-18c0-472a-a86d-2258cb558b7e .toggle", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-e28f6256-Button_4")) {
      if(jFirer.data("jimHasToggle")) {
        jFirer.removeData("jimHasToggle");
        jEvent.undoCases(jFirer);
      } else {
        jFirer.data("jimHasToggle", true);
        event.backupState = true;
        event.target = jFirer;
        cases = [
          {
            "blocks": [
              {
                "actions": [
                  {
                    "action": "jimChangeStyle",
                    "parameter": [ {
                      "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                        "attributes": {
                          "background-color": "#999999",
                          "background-image": "none",
                          "border-top-width": "2px",
                          "border-top-style": "solid",
                          "border-top-color": "#999999",
                          "border-right-width": "2px",
                          "border-right-style": "solid",
                          "border-right-color": "#999999",
                          "border-bottom-width": "2px",
                          "border-bottom-style": "solid",
                          "border-bottom-color": "#999999",
                          "border-left-width": "2px",
                          "border-left-style": "solid",
                          "border-left-color": "#999999",
                          "border-radius": "0px 0px 0px 0px",
                          "font-size": "11.25pt",
                          "font-family": "'Segoe UI',Arial"
                        }
                      }
                    },{
                      "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4 .valign": {
                        "attributes": {
                          "vertical-align": "middle",
                          "text-align": "center"
                        }
                      }
                    },{
                      "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4 span": {
                        "attributes": {
                          "color": "#000000",
                          "text-align": "center",
                          "text-decoration": "none",
                          "font-family": "'Segoe UI',Arial",
                          "font-size": "11.25pt",
                          "font-style": "normal",
                          "font-weight": "400"
                        }
                      }
                    },{
                      "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                        "attributes-ie": {
                          "border-top-width": "2px",
                          "border-top-style": "solid",
                          "border-top-color": "#999999",
                          "border-right-width": "2px",
                          "border-right-style": "solid",
                          "border-right-color": "#999999",
                          "border-bottom-width": "2px",
                          "border-bottom-style": "solid",
                          "border-bottom-color": "#999999",
                          "border-left-width": "2px",
                          "border-left-style": "solid",
                          "border-left-color": "#999999",
                          "border-radius": "0px 0px 0px 0px",
                          "-pie-background": "#999999",
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
        jEvent.launchCases(cases);
      }
    }
  })
  .on("pageload", ".m-e28f6256-18c0-472a-a86d-2258cb558b7e .pageload", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-e28f6256-Recorded_ScreensM_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": {
                "action": "jimEquals",
                "parameter": [ null,"1" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-e28f6256-Recorded_Screens_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-e28f6256-Recorded_ScreensM_Panel" ]
                  },
                  "exectype": "parallel",
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
  .on("mouseenter dragenter", ".m-e28f6256-18c0-472a-a86d-2258cb558b7e .mouseenter", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#m-e28f6256-Button_4")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                      "attributes": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#0A457F",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#0A457F",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#0A457F",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#0A457F",
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                      "attributes-ie": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#0A457F",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#0A457F",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#0A457F",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#0A457F",
                        "border-radius": "0px 0px 0px 0px"
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
    }
  })
  .on("mouseleave dragleave", ".m-e28f6256-18c0-472a-a86d-2258cb558b7e .mouseleave", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#m-e28f6256-Button_4") && (jQuery(document.elementFromPoint(event.clientX, event.clientY)).closest("#m-e28f6256-Button_4").length === 0 || jQuery(document.elementFromPoint(event.clientX, event.clientY)).closest("#m-e28f6256-Button_4") !== jFirer)) {
      event.stopPropagation();
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                      "attributes": {
                        "background-color": "#1974D5",
                        "background-image": "none",
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#1974D5",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#1974D5",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#1974D5",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#1974D5",
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-e28f6256-18c0-472a-a86d-2258cb558b7e #m-e28f6256-Button_4": {
                      "attributes-ie": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#1974D5",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#1974D5",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#1974D5",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#1974D5",
                        "border-radius": "0px 0px 0px 0px",
                        "-pie-background": "#1974D5",
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
    }
  });