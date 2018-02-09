MVCApp.factory('blockUIFactory', blockUIFactory);

function blockUIFactory(blockUI)
{
    var factory = {
        StartBlock: StartBlock,
        StopBlock: StopBlock
    };
    return factory;

    function StartBlock(blockID)
    {
        var BlockObject = blockUI.instances.get(blockID);
        BlockObject.start();
    }

    function StopBlock(blockID)
    {
        var BlockObject = blockUI.instances.get(blockID);
        BlockObject.stop();
    }

}