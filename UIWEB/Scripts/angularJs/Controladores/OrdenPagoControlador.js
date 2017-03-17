myApp.controller('OrdenPagoControlador', ['$http', '$uibModal', 'DTOptionsBuilder', 'DTColumnDefBuilder', function ($http, $uibModal, DTOptionsBuilder, DTColumnDefBuilder) {
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

    debugger;

    $http({
        method: 'GET',
        url: '../../../OrdenPago/GetOrdenPagos'
    }).then(function (result) {
        vm.OrdenPagos = result.data;
        console.log(vm.OrdenPagos);
        debugger;
    }, function (error) {
        console.log(result);
    });

    $http({
        method: 'GET',
        url: '../../../OrdenPago/GetOrdenPagoNuevo'
    }).then(function (result) {
        vm.OrdenPagoNuevo = result.data;
        console.log('OrdenPago nuevo');
        console.log(vm.OrdenPagoNuevo);
        debugger;
    }, function (error) {
        console.log(result);
    });


    this.Editar = function (data, indice) {
        console.log('editar pop up');

        data.Indice = indice;

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/OrdenPago/Editar.html'
             , controller: 'OrdenPagoEditarControlador as editarCtrl'
            , resolve: {

                OrdenPago: function () {
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
        $http.post('../../../OrdenPago/Delete', data).then(function (response) {
            console.log('exito');
            vm.OrdenPagos.splice(indice, 1);

        }, function () { console.log('error post') });
    };

    vm.Nuevo = function () {
        var OrdenPago = vm.OrdenPagoNuevo;
        console.log('pasando objeto OrdenPago Nuevo');
        console.log(vm.OrdenPagoNuevo);

        var modalInstance = $uibModal.open({
            templateUrl: '../Template/HTML/OrdenPago/Nuevo.html'
             , controller: 'OrdenPagoNuevoControlador'
            , resolve: {
                OrdenPago: function () {
                    return OrdenPago;
                }
            }
        });

        modalInstance.result.then(function (response) {
            vm.OrdenPagos.push(response.data.obj);
            debugger;
        });
    }
}]);


myApp.controller('OrdenPagoNuevoControlador', ['$scope', '$http', '$uibModalInstance', 'OrdenPago', function ($scope, $http, $uibModalInstance, OrdenPago) {
    $scope.close = function () {
        $uibModalInstance.dismiss('cancel');
    };
    //----LIMPIANDO VALORES
    $scope.OrdenPago = OrdenPago;


    //----Validacion
    $scope.AlertaTipo = 'danger';
    $scope.AlertaMensaje = '';
    $scope.MostrarValidacion = false;
    $scope.CloseAlert = function (e) {
        $scope.MostrarValidacion = false;
    }
    //--------------------------
    $scope.OrdenPago.Moneda = OrdenPago.CfgMonedas[0];
    $scope.OrdenPago.Estado = OrdenPago.CfgEstados[0];
    $scope.OrdenPago.Sucursal = OrdenPago.CfgSucursals[0];

    $scope.OrdenPago.Nombre = "";
    $scope.OrdenPago.FechaRegistroString = "";
    $scope.OrdenPago.Direccion = "";
    $scope.OrdenPago.CfgBancos = OrdenPago.CfgBancos;
    console.log('CfgBancos' + $scope.OrdenPago.CfgBancos);
    console.log('abriendo pop up nuevo producto');
    console.log($scope.OrdenPago);

    $scope.GuardarNuevoOrdenPago = function () {
        console.log('guardando nuevo OrdenPago');
        console.log($scope.OrdenPago);


        $http.post('../../../OrdenPago/Insert', $scope.OrdenPago).then(function (response) {
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
