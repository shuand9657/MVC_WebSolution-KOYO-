MVCApp.factory('ToolTipFactory',TooltipFactory);

function TooltipFactory(){
    var factory = {
        ToolTipFor: ToolTipFor
    };
    return factory;

    function ToolTipFor(content)
    {
        return "<p align='left' ng-repeat='msg in " + content + "' >*{{msg.message}}</p>";
    };
}