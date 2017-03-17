/// <reference path="D:\proyectos\ejemplos\ElComercio\vsPruebaErnestoGalarza\UIWEB\Template/HTML/Banco/Nuevo.html" />

myApp.controller('BancoControlador', ['$http', '$uibModal', 'DTOptionsBuilder', 'DTColumnDefBuilder', function ($http, $uibModal, DTOptionsBuilder, DTColumnDefBuilder) {
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


    debugger;

    $http({
        method: 'GET',
        url: '../../../Banco/GetBancos'
    }).then(function (result) {
        vm.Bancos = result.data;
        debugger;
    }, function (error) {
        console.log(result);
    });

    $http({
        method: 'GET',
        url: '../../../Banco/GetBancoNuevo'
    }).then(function (result) {
        vm.BancoNuevo = result.data;
        console.log('Banco nuevo');
        console.log(vm.BancoNuevo);
        debugger;
    }, function (error) {
        console.log(result);
    });


    this.Editar = function (data, indice) {
        console.log('editar pop up');

        data.Indice = indice;

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/Banco/Editar.html'
             , controller: 'BancoEditarControlador as editarCtrl'
            , resolve: {

                Banco: function () {
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
        $http.post('../../../Banco/Delete', data).then(function (response) {

            if (response.data.success) {
                console.log('exito');
                vm.Bancos.splice(indice, 1);

            } else {
                vm.MostrarValidacion = true;
                vm.AlertaMensaje = response.data.message;

            }      




        }, function () {
            vm.MostrarValidacion = true;
            vm.AlertaMensaje = 'Error';
            console.log('error')
        });
    };

    vm.Nuevo = function () {
        var Banco = vm.BancoNuevo;
        console.log('pasando objeto Banco Nuevo');
        console.log(vm.BancoNuevo);

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/Banco/Nuevo.html'
             , controller: 'BancoNuevoControlador'
            , resolve: {
                Banco: function () {
                    return Banco;
                }
            }
        });

        modalInstance.result.then(function (response) {
            vm.Bancos.push(response.data.obj);
            debugger;
        });
    }
}]);


myApp.controller('BancoNuevoControlador', ['$scope', '$http', '$uibModalInstance', 'Banco', function ($scope, $http, $uibModalInstance, Banco) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
    //----LIMPIANDO VALORES
    $scope.Banco = Banco;

    $scope.Banco.Nombre = "";
    $scope.Banco.FechaRegistroString = "";
    $scope.Banco.Direccion = "";

    //----Validacion
    $scope.AlertaTipo = 'danger';
    $scope.AlertaMensaje = '';
    $scope.MostrarValidacion = false;
    $scope.CloseAlert = function (e) {
        $scope.MostrarValidacion = false;
    }
    //--------------------------

    console.log('abriendo pop up nuevo producto');
    console.log(Banco);
    $scope.PintandoLog = function () {
        console.log('guardando nuevo combo');
        console.log($scope.Banco);

        console.log('.....despuesssss');

        console.log($scope.Banco);
    }
    $scope.GuardarNuevoBanco = function () {
        console.log('guardando nuevo banco');
        console.log($scope.Banco);


        $http.post('../../../Banco/Insert', $scope.Banco).then(function (response) {
         
            if (response.data.success)
                $uibModalInstance.close(response);
            else {
                $scope.MostrarValidacion = true;
                $scope.AlertaMensaje = response.data.message;
            }

        }, function () {
            $scope.MostrarValidacion = true;
            $scope.AlertaMensaje = response;
            console.log('error post');
        });

    }

}]);

