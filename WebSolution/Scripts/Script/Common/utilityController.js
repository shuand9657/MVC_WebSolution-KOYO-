MVCApp.controller('utilityCtrl', utilityCtrl);

function utilityCtrl($scope , $http,blockUI){
    var vm = this;
    vm.toolbarOptions= {};
    vm.formatOptions={};
    vm.TreeViewDatasource  ={};
    vm.treeOptions = {};

    vm.treeOptions = {
        loadOnDemand: true,
        dataSource : vm.TreeViewDatasource,
        dataTextField: 'text'
    };

    vm.TreeViewDatasource = new kendo.data.HierarchicalDataSource({
        transport:{
            read: function (options) {
                if (options.data.id) {
                    var getID = options.data.id;
                    var i = getID.split("-");
                    var nodeID = i[1];
                    var items = vm.dynamicTreeViewDatasource.get(options.data.id);
                    
                    console.log(JSON.stringify(items));
                    console.log(items);
                }
                else var items = { Level: null };

                $http({
                    method: 'post',
                    url: 'tvonDemand',
                    data: { nodeID: nodeID, Level: items.Level ,rootID:items.rootID},
                    dataType: 'json'
                }).success(function (data, status, headers, config) {
                    options.success(data);
                }).error(function (data, status, headers, config) {
                    options.error(status);
                    alert('Error getting tree view Data!' + status);
                });
            }
        },
        schema:{
            data:'items',
            model:{
                id: 'id',
                text: 'text',
                uid:'uid',
                hasChildren: 'hasChildren'
            }
        }
    });

    vm.toolbarOptions ={
        items: [
                  { type: "button", text: "Button" },
                  { type: "button", text: "Toggle Button", togglable: true },
                  {
                      type: "splitButton",
                      text: "Insert",
                      menuButtons: [
                          { text: "Insert above", icon: "insert-n" },
                          { text: "Insert between", icon: "insert-m" },
                          { text: "Insert below", icon: "insert-s" }
                      ]
                  },
                  { type: "separator" },
                  { template: "<label>Format:</label>" },
                  {
                      template: "<input kendo-drop-down-list k-options='vm.formatOptions' style='width: 150px;' />",
                      overflow: "never"
                  },
                  { type: "separator" },
                  {
                      type: "buttonGroup",
                      buttons: [
                          { spriteCssClass: "k-tool-icon k-justifyLeft", text: "Left", togglable: true, group: "text-align" },
                          { spriteCssClass: "k-tool-icon k-justifyCenter", text: "Center", togglable: true, group: "text-align" },
                          { spriteCssClass: "k-tool-icon k-justifyRight", text: "Right", togglable: true, group: "text-align" }
                      ]
                  },
                  {
                      type: "buttonGroup",
                      buttons: [
                          { spriteCssClass: "k-tool-icon k-bold", text: "Bold", togglable: true, showText: "overflow" },
                          { spriteCssClass: "k-tool-icon k-italic", text: "Italic", togglable: true, showText: "overflow" },
                          { spriteCssClass: "k-tool-icon k-underline", text: "Underline", togglable: true, showText: "overflow" }
                      ]
                  },
                  {
                      type: "button",
                      text: "Action",
                      overflow: "always"
                  },
                  {
                      type: "button",
                      text: "Another Action",
                      overflow: "always"
                  },
                  {
                      type: "button",
                      text: "Something else here",
                      overflow: "always"
                  }
              ]
    };
    vm.formatOptions = {
        optionLabel: "Paragraph",
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "Heading 1", value: 1 },
            { text: "Heading 2", value: 2 },
            { text: "Heading 3", value: 3 },
            { text: "Title", value: 4 },
            { text: "Subtitle", value: 5 },
        ]
    };




}