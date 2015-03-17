HeartbeatApp.controller("HeartbeatController", function AppController($scope, $location, HeartbeatService) {
   
    $scope.menuItems = [{ name: 'Home', cls: 'nav navbar-item active', url: 'home/index' }, { name: 'About Us', cls: 'nav navbar-item ', url: 'home/about' }, { name: 'Products', cls: 'nav navbar-item ', url: 'home/Products' }, { name: 'Services', cls: 'nav navbar-item', url: 'home/Services' }, { name: 'Goals and Vision', cls: 'nav navbar-item', url: 'home/Goals' }, { name: 'Meet The Team', cls: 'nav navbar-item', url: 'home/Team' }, { name: 'Contact Us', cls: 'nav navbar-item', url: 'Home/ContactUs' }];
    $scope.app = '';
   
    $scope.url = $location.host();
    $scope.path = $location.path();
    $scope.port = $location.port();
    $scope.homePath=$scope.url+':'+$scope.port+'/'+$scope.menuItems[0].url
    $scope.init = function () {

        $scope.constructMenu();

    };
    //$scope.loginSucess = function (response) {
    //    $scope.User = response;
    //    if ($scope.User.UserId > 0) {
    //        $scope.menuItems = [{ name: 'Schedule', cls: 'nav active', url: 'home/index' }, { name: 'Appointment', cls: 'nav', url: 'home/about', submenu: [{ name: 'Vision', url: 'home/about' }, { name: 'Meet The Team', url: 'home/team' }] }, { name: 'Admin', cls: 'nav', url: 'Home/Contact' }];

    //        $scope.constructMenu();
    //        $scope.$apply();
    //       window.location.href = "/Scheduling?session="+$scope.User.UserId;
    //    }
    //};

    //$scope.ErrorLogin = function (error) {
    //    alert('something went wrong');
    //};

    //$scope.ValidateUser = function () {
 
    //    var obj = { "UserName": "Waleed", "Password": "welcome" };
    //   obj.UserName=$('#user').val();
    //   obj.Password = $('#pass').val();
    //   HeartbeatService.PostData($scope.loginSucess, $scope.ErrorLogin, 'Security',obj );
       
    //};
   
    $scope.constructMenu = function () {
        var url = $location.absUrl();
       
        var start = url.toLowerCase().indexOf("home");
        
        var page = url.substring(start);
        
        var app = document.getElementById('app').value;
        if (app === 'Scheduling')
            $scope.menuItems = [{ name: 'Dashboard', cls: 'nav active', url: 'Scheduling/index' }, { name: 'Customers', cls: 'nav active', url: 'Scheduling/Customer' }, { name: 'Calendar', cls: 'nav active', url: 'Scheduling/Daily' }, { name: 'Projects', cls: 'nav active', url: 'Scheduling/Project' }, { name: 'BPM', cls: 'nav active', url: 'Scheduling/Workflow' }, { name: 'Administration', cls: 'nav active', url: 'Scheduling/Admin', submenu: [{ name: 'Professional', url: 'Scheduling/Professional' }, { name: 'Insurance', url: 'Scheduling/Insurance' }, { name: 'Workflow Admin', url: 'Scheduling/WorkflowAdmin' }] }]
        var mainMenu = document.getElementById('menu');
        var menuItem;
        var submenu;
        var submenuitem;
        mainMenu.innerHTML='';
        for (var i = 0; i < $scope.menuItems.length; i++) {

            menuItem = document.createElement('li');

            if ($scope.menuItems[i].submenu === undefined) {
                
                if (start === -1)
                    page = 'home/index';
                if (page.toLowerCase().indexOf($scope.menuItems[i].url.toLowerCase()) === 0)
                    $scope.menuItems[i].cls = 'nav navbar-item active';
                
                    else

                    $scope.menuItems[i].cls = 'nav navbar-item';


                menuItem.setAttribute("class", $scope.menuItems[i].cls);
              
                    menuItem.innerHTML = '<a style="color:white;" href=http://' + $scope.url + ':' + $scope.port + '/' + $scope.menuItems[i].url + '/>' + $scope.menuItems[i].name + '</a>';
            }
            else {
                menuItem.setAttribute("class", "dropdown");
                
                menuItem.onmouseover = function () { this.setAttribute("class", "dropdown open dropText "); };
                menuItem.onmouseout = function () { this.setAttribute("class", "dropdown dropText"); };
                menuItem.innerHTML = '<a data-toggle=dropdown class="dropdown-toggle dropText" href=http://' + $scope.url + ':' + $scope.port + '/' + $scope.menuItems[i].url + '/>' + $scope.menuItems[i].name + '<b class=caret></b></a>';
                submenu = document.createElement('ul');
                submenu.setAttribute("class", "dropdown-menu dropText");
                for (j = 0; j < $scope.menuItems[i].submenu.length; j++)
                {
                    submenuitem = document.createElement('li');
                    submenuitem.setAttribute("class", "subitem");
                    submenuitem.innerHTML = '<a  class="dropText" href=http://' + $scope.url + ':' + $scope.port + '/' + $scope.menuItems[i].submenu[j].url + '/>' + $scope.menuItems[i].submenu[j].name + '</a>';
                    submenu.appendChild(submenuitem);
                }
                menuItem.appendChild(submenu);
            }
            mainMenu.appendChild(menuItem);
        }


    };
    //End of Scope Function
}
);