'use strict';

MVCApp.service('ClientLogService',clientLogService);

function clientLogService(){
    var vm = this;

    var logRecord = 100;
    var Log = log;
    //overload type
    //vm.ToggleLogService = toggleLogService;
    //vm.SetLogRecord = setLogRecord;
    //vm.GetLogRecord = getLogRecord;
    //vm.DoLogService = doLogService;

    function toggleLogService(){return true;}
    
    function setLogRecord(logNum){logRecord = logNum;}

    function getLogRecord(){return logRecord;}

    function doLogService(userAction){
        //record user action 
    }
    //function log(a,b,c) javascript overloda
}