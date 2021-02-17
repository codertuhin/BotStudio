jQuery("#simulation")
  .on("click", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .click", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-aa0e3abb-Input_13")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimFocusOn",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_13" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
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
    } else if(jFirer.is("#m-aa0e3abb-Image_101")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "variable": [ "Change_MainContent_Win" ],
                    "value": "4"
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
    } else if(jFirer.is("#m-aa0e3abb-Image_2")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_5" ],
                    "value": {
                      "action": "jimPlus",
                      "parameter": [ {
                        "datatype": "property",
                        "target": "#m-aa0e3abb-Input_5",
                        "property": "jimGetValue"
                      },"1" ]
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Image_3")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_5" ],
                    "value": {
                      "action": "jimMinus",
                      "parameter": [ {
                        "datatype": "property",
                        "target": "#m-aa0e3abb-Input_5",
                        "property": "jimGetValue"
                      },"1" ]
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Image_4")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_10" ],
                    "value": {
                      "action": "jimPlus",
                      "parameter": [ {
                        "datatype": "property",
                        "target": "#m-aa0e3abb-Input_10",
                        "property": "jimGetValue"
                      },"1" ]
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Image_5")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_10" ],
                    "value": {
                      "action": "jimMinus",
                      "parameter": [ {
                        "datatype": "property",
                        "target": "#m-aa0e3abb-Input_10",
                        "property": "jimGetValue"
                      },"1" ]
                    }
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
  .on("focusin", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .focusin", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-aa0e3abb-Input_6")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_6": {
                      "attributes": {
                        "background-color": "#175A90",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_6": {
                      "attributes-ie": {
                        "-pie-background": "#175A90",
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
    } else if(jFirer.is("#m-aa0e3abb-Input_7")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_7": {
                      "attributes": {
                        "background-color": "#175A90",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_7": {
                      "attributes-ie": {
                        "-pie-background": "#175A90",
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
    } else if(jFirer.is("#m-aa0e3abb-Input_8")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_8": {
                      "attributes": {
                        "background-color": "#175A90",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_8": {
                      "attributes-ie": {
                        "-pie-background": "#175A90",
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
    } else if(jFirer.is("#m-aa0e3abb-Input_9")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_9": {
                      "attributes": {
                        "background-color": "#175A90",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_9": {
                      "attributes-ie": {
                        "-pie-background": "#175A90",
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
    } else if(jFirer.is("#m-aa0e3abb-Input_5")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_23": {
                      "attributes": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#74B9EF",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#74B9EF",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#74B9EF",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#74B9EF",
                        "border-radius": "3px 3px 3px 3px",
                        "padding-top": "2px",
                        "padding-right": "2px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions": {
                        "width": "Math.max(168 - 1 - 1 - 2 - 2, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_23": {
                      "attributes-ie": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#74B9EF",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#74B9EF",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#74B9EF",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#74B9EF",
                        "border-radius": "3px 3px 3px 3px",
                        "padding-top": "2px",
                        "padding-right": "2px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions-ie": {
                        "width": "Math.max(168 - 1 - 1 - 2 - 2, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
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
    } else if(jFirer.is("#m-aa0e3abb-Input_10")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_24": {
                      "attributes": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#74B9EF",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#74B9EF",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#74B9EF",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#74B9EF",
                        "border-radius": "3px 3px 3px 3px",
                        "padding-top": "2px",
                        "padding-right": "2px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions": {
                        "width": "Math.max(168 - 1 - 1 - 2 - 2, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_24": {
                      "attributes-ie": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#74B9EF",
                        "border-right-width": "1px",
                        "border-right-style": "solid",
                        "border-right-color": "#74B9EF",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#74B9EF",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#74B9EF",
                        "border-radius": "3px 3px 3px 3px",
                        "padding-top": "2px",
                        "padding-right": "2px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions-ie": {
                        "width": "Math.max(168 - 1 - 1 - 2 - 2, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
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
  .on("focusout", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .focusout", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-aa0e3abb-Input_6")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_13" ],
                    "value": {
                      "datatype": "property",
                      "target": "#m-aa0e3abb-Input_6",
                      "property": "jimGetValue"
                    }
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_6": {
                      "attributes": {
                        "background-color": "transparent",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_6": {
                      "attributes-ie": {
                        "-pie-background": "transparent",
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
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
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
    } else if(jFirer.is("#m-aa0e3abb-Input_7")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_13" ],
                    "value": {
                      "datatype": "property",
                      "target": "#m-aa0e3abb-Input_7",
                      "property": "jimGetValue"
                    }
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_7": {
                      "attributes": {
                        "background-color": "transparent",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_7": {
                      "attributes-ie": {
                        "-pie-background": "transparent",
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
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
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
    } else if(jFirer.is("#m-aa0e3abb-Input_8")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_13" ],
                    "value": {
                      "datatype": "property",
                      "target": "#m-aa0e3abb-Input_8",
                      "property": "jimGetValue"
                    }
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_8": {
                      "attributes": {
                        "background-color": "transparent",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_8": {
                      "attributes-ie": {
                        "-pie-background": "transparent",
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
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
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
    } else if(jFirer.is("#m-aa0e3abb-Input_9")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimSetValue",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_13" ],
                    "value": {
                      "datatype": "property",
                      "target": "#m-aa0e3abb-Input_9",
                      "property": "jimGetValue"
                    }
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_9": {
                      "attributes": {
                        "background-color": "transparent",
                        "background-image": "none"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Input_9": {
                      "attributes-ie": {
                        "-pie-background": "transparent",
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
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
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
    } else if(jFirer.is("#m-aa0e3abb-Input_13")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimPause",
                  "parameter": {
                    "pause": 100
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimHide",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Dynamic_Panel_3" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                },
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes": {
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
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
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
    } else if(jFirer.is("#m-aa0e3abb-Input_5")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_23": {
                      "attributes": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#CCCCCC",
                        "border-right-width": "0px",
                        "border-right-style": "none",
                        "border-right-color": "#000000",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#CCCCCC",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#CCCCCC",
                        "border-radius": "3px 0px 0px 3px",
                        "padding-top": "2px",
                        "padding-right": "0px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions": {
                        "width": "Math.max(168 - 1 - 0 - 2 - 0, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_23": {
                      "attributes-ie": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#CCCCCC",
                        "border-right-width": "0px",
                        "border-right-style": "none",
                        "border-right-color": "#000000",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#CCCCCC",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#CCCCCC",
                        "border-radius": "3px 0px 0px 3px",
                        "padding-top": "2px",
                        "padding-right": "0px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions-ie": {
                        "width": "Math.max(168 - 1 - 0 - 2 - 0, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
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
    } else if(jFirer.is("#m-aa0e3abb-Input_10")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_24": {
                      "attributes": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#CCCCCC",
                        "border-right-width": "0px",
                        "border-right-style": "none",
                        "border-right-color": "#000000",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#CCCCCC",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#CCCCCC",
                        "border-radius": "3px 0px 0px 3px",
                        "padding-top": "2px",
                        "padding-right": "0px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions": {
                        "width": "Math.max(168 - 1 - 0 - 2 - 0, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Rectangle_24": {
                      "attributes-ie": {
                        "border-top-width": "1px",
                        "border-top-style": "solid",
                        "border-top-color": "#CCCCCC",
                        "border-right-width": "0px",
                        "border-right-style": "none",
                        "border-right-color": "#000000",
                        "border-bottom-width": "1px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#CCCCCC",
                        "border-left-width": "1px",
                        "border-left-style": "solid",
                        "border-left-color": "#CCCCCC",
                        "border-radius": "3px 0px 0px 3px",
                        "padding-top": "2px",
                        "padding-right": "0px",
                        "padding-bottom": "2px",
                        "padding-left": "2px"
                      },
                      "expressions-ie": {
                        "width": "Math.max(168 - 1 - 0 - 2 - 0, 0) + 'px'",
                        "height": "Math.max(28 - 1 - 1 - 2 - 2, 0) + 'px'"
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
  .on("mouseenter dragenter", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .mouseenter", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#m-aa0e3abb-Panel_4")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#666666",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#666666",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#666666",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#666666",
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes-ie": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#666666",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#666666",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#666666",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#666666",
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
    } else if(jFirer.is("#m-aa0e3abb-Input_13")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes": {
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
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
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
  .on("mouseleave dragleave", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .mouseleave", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getDirectEventFirer(this);
    if(jFirer.is("#m-aa0e3abb-Input_13") && (jQuery(document.elementFromPoint(event.clientX, event.clientY)).closest("#m-aa0e3abb-Input_13").length === 0 || jQuery(document.elementFromPoint(event.clientX, event.clientY)).closest("#m-aa0e3abb-Input_13") !== jFirer)) {
      event.stopPropagation();
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimChangeStyle",
                  "parameter": [ {
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#616161",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#616161",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#616161",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#616161",
                        "border-radius": "0px 0px 0px 0px"
                      }
                    }
                  },{
                    "#m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 #m-aa0e3abb-Label_21": {
                      "attributes-ie": {
                        "border-top-width": "2px",
                        "border-top-style": "solid",
                        "border-top-color": "#616161",
                        "border-right-width": "2px",
                        "border-right-style": "solid",
                        "border-right-color": "#616161",
                        "border-bottom-width": "2px",
                        "border-bottom-style": "solid",
                        "border-bottom-color": "#616161",
                        "border-left-width": "2px",
                        "border-left-style": "solid",
                        "border-left-color": "#616161",
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
  .on("windowresize", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .windowresize", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-aa0e3abb-Input_6")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimResize",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_6" ],
                    "width": {
                      "type": "parentrelative",
                      "value": "100"
                    },
                    "height": {
                      "type": "noresize"
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Input_7")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimResize",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_7" ],
                    "width": {
                      "type": "parentrelative",
                      "value": "100"
                    },
                    "height": {
                      "type": "noresize"
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Input_8")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimResize",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_8" ],
                    "width": {
                      "type": "parentrelative",
                      "value": "100"
                    },
                    "height": {
                      "type": "noresize"
                    }
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
    } else if(jFirer.is("#m-aa0e3abb-Input_9")) {
      cases = [
        {
          "blocks": [
            {
              "actions": [
                {
                  "action": "jimResize",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Input_9" ],
                    "width": {
                      "type": "parentrelative",
                      "value": "100"
                    },
                    "height": {
                      "type": "noresize"
                    }
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
  .on("variablechange.jim", ".m-aa0e3abb-c388-4de2-8a06-8eb74103bee7 .variablechange", function(event, data) {
    var jEvent, jFirer, cases;
    if(data === undefined) { data = event; }
    jEvent = jimEvent(event);
    jFirer = jEvent.getEventFirer();
    if(jFirer.is("#m-aa0e3abb-OpenTask_Panel")) {
      cases = [
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_Win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_Win"
                },"1" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-All_Tasks_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_Win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_Win"
                },"2" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Dashboard_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_Win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_Win"
                },"3" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-Insights_Panel" ]
                  },
                  "exectype": "serial",
                  "delay": 0
                }
              ]
            }
          ],
          "exectype": "serial",
          "delay": 0
        },
        {
          "blocks": [
            {
              "condition": (data.variableTarget === "Change_MainContent_Win") && {
                "action": "jimEquals",
                "parameter": [ {
                  "datatype": "variable",
                  "element": "Change_MainContent_Win"
                },"4" ]
              },
              "actions": [
                {
                  "action": "jimShow",
                  "parameter": {
                    "target": [ "#m-aa0e3abb-OpenTask_Panel","#m-aa0e3abb-Open_task_actions" ]
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