(function(){
    'use strict';
    angular.module('DemoApp').controller('DemoCtrl',DemoCtrl);
})();

function DemoCtrl (){
    var vm = this;
    vm.FeelingOption2 = [];
    vm.md = {
        secondLabel: "Label Item # 2(long content)",
        longContent: "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla venenatis ante augue. Phasellus volutpat neque ac dui mattis vulputate. Etiam consequat aliquam cursus. In sodales pretium ultrices. Maecenas lectus est, sollicitudin consectetur felis nec, feugiat ultricies mi. Aliquam erat volutpat. Nam placerat, tortor in ultrices porttitor, orci enim rutrum enim, vel tempor sapien arcu a tellus. Vivamus convallis sodales ante varius gravida. Curabitur a purus vel augue ultrices ultricies id a nisl. Nullam malesuada consequat diam, a facilisis tortor volutpat et. Sed urna dolor, aliquet vitae posuere vulputate, euismod ac lorem. Sed felis risus, pulvinar at interdum quis, vehicula sed odio. Phasellus in enim venenatis, iaculis tortor eu, bibendum ante. Donec ac tellus dictum neque volutpat blandit. Praesent efficitur faucibus risus, ac auctor purus porttitor vitae. Phasellus ornare dui nec orci posuere, nec luctus mauris semper.</p> "
    };

    
    vm.FeelingOption2.push({
        feelOption: [
            { Value: 0, Text: "Select an options.." },
        { Value: 1, Text: "Happy" },
        { Value: 2, Text: "Tired" },
        { Value: 3, Text: "Angry" },
        { Value: 4, Text: "Sad" }
        ]
    });
    //vm.FeelingOption = [];
    vm.FeelingOption=[
        {Value : 0 , Text : "Select an options.."},
        {Value :1 , Text: "Happy"},
        {Value : 2 , Text :"Tired"},
        {Value: 3, Text: "Angry"},
        {Value : 4, Text:"Sad"}
    ];
    console.log(vm.FeelingOption);
    console.log(vm.FeelingOption2);

    vm.users = [
    { id: 1, name: 'Bob' },
    { id: 2, name: 'Alice' },
    { id: 3, name: 'Steve' }
    ];
    vm.selectedUser = { id: 1, name: 'Bob' };
};