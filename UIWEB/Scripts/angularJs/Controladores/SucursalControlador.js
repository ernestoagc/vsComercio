myApp.controller('SucursalControlador', ['$http', '$uibModal', 'DTOptionsBuilder', 'DTColumnDefBuilder', function ($http, $uibModal, DTOptionsBuilder, DTColumnDefBuilder) {
    vm = this;

    vm.dtOptions = DTOptionsBuilder.newOptions()
	.withPaginationType('full_numbers')
	.withDisplayLength(10)
	.withDOM('ftipr')
	.withOption('bLengthChange', true)
	.withOption('bInfo', true)
	.withOption('bFilter', true)
	.withOption('bAutoWidth', true)
	.withOption('bPaginate', true);
    vm.dtInstance = {};


    //----Validacion
    vm.AlertaTipo = 'danger';
    vm.AlertaMensaje = '';
    vm.MostrarValidacion = false;
    vm.CloseAlert = function (e) {
        vm.MostrarValidacion = false;
    }
    //--------------------------


    $http({
        method: 'GET',
        url: '../../../Sucursal/GetSucursals'
    }).then(function (result) {
        vm.Sucursals = result.data;
        console.log(vm.Sucursals);
        debugger;
    }, function (error) {
        console.log(result);
    });

    $http({
        method: 'GET',
        url: '../../../Sucursal/GetSucursalNuevo'
    }).then(function (result) {
        vm.SucursalNuevo = result.data;
        console.log('Sucursal nuevo');
        console.log(vm.SucursalNuevo);
        debugger;
    }, function (error) {
        console.log(result);
    });


    this.Editar = function (data, indice) {
        console.log('editar pop up');

        data.Indice = indice;

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/Sucursal/Editar.html'
             , controller: 'SucursalEditarControlador as editarCtrl'
            , resolve: {

                Sucursal: function () {
                    return data
                    console.log('pasand objeto data al edit');
                }

            }
        });

        modalInstance.result.then(function (response) {
            console.log('actualizando objeto');
            vm.Productos[response.data.Indice] = response.data;

            debugger;
        });


    }

    vm.Eliminar = function (data, indice) {
        console.log('eliminando');
        // data.Indice = indice;
        debugger;
        $http.post('../../../Sucursal/Delete', data).then(function (response) {
            if (response.data.success) {
                console.log('exito');
                vm.Sucursals.splice(indice, 1);

            } else {
                vm.MostrarValidacion = true;
                vm.AlertaMensaje = response.data.message;

            }
            


        }, function () {
            vm.MostrarValidacion = true;
            vm.AlertaMensaje = 'ERROR';
            console.log('error post')
        });
    };

    vm.Nuevo = function () {
        var Sucursal = vm.SucursalNuevo;
        console.log('pasando objeto Sucursal Nuevo');
        console.log(vm.SucursalNuevo);

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/Sucursal/Nuevo.html'
             , controller: 'SucursalNuevoControlador'
            , resolve: {
                Sucursal: function () {
                    return Sucursal;
                }
            }
        });

        modalInstance.result.then(function (response) {
            vm.Sucursals.push(response.data.obj);
            debugger;
        });
    }
}]);


myApp.controller('SucursalNuevoControlador', ['$scope', '$http', '$uibModalInstance', 'Sucursal', function ($scope, $http, $uibModalInstance, Sucursal) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
    //----LIMPIANDO VALORES
    $scope.Sucursal = Sucursal;


    //----Validacion
    $scope.AlertaTipo = 'danger';
    $scope.AlertaMensaje = '';
    $scope.MostrarValidacion = false;
    $scope.CloseAlert = function (e) {
        $scope.MostrarValidacion = false;
    }
    //--------------------------
    $scope.Sucursal.Banco = Sucursal.CfgBancos[0];

    $scope.Sucursal.Nombre = "";
    $scope.Sucursal.FechaRegistroString = "";
    $scope.Sucursal.Direccion = "";
    $scope.Sucursal.CfgBancos = Sucursal.CfgBancos;
    console.log('CfgBancos' + $scope.Sucursal.CfgBancos);
    console.log('abriendo pop up nuevo producto');
    console.log($scope.Sucursal);
   
    $scope.GuardarNuevoSucursal = function () {
        console.log('guardando nuevo Sucursal');
        console.log($scope.Sucursal);


        $http.post('../../../Sucursal/Insert', $scope.Sucursal).then(function (response) {
            debugger;
            if (response.data.success)
                $uibModalInstance.close(response);
            else {
                $scope.MostrarValidacion = true;
                $scope.AlertaMensaje = response.data.message;
            }
        }, function () {
            $scope.MostrarValidacion = true;
            $scope.AlertaMensaje = response;
        });

    }

}]);
