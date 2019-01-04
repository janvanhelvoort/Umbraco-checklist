function ContentAppController($scope, editorState, checklistResource) {
    $scope.content = { task: { contentNodeId: editorState.current.id }, tasks: [] };

    checklistResource.getTasks(editorState.current.id).then(function (result) {
        $scope.content.tasks = result.data;
    });

    $scope.enterSubmitTask = function ($event) {
        if ($event.which === 13 && $scope.content.task.content) {
            this.addTask();
        }
    };

    $scope.addTask = function () {
        $scope.actionInProgress = true;

        checklistResource.saveTask($scope.content.task).then(function (result) {
            $scope.content.tasks.push(result.data);
            $scope.content.task.content = '';

            $scope.actionInProgress = false;
        });
    };

    $scope.selectItem = function (task, $event) {
        $scope.actionInProgress = true;

        checklistResource.saveTask(Object.assign({}, task, { isReady: !task.isReady })).then(function (result) {
            task.isReady = result.data.isReady;

            $scope.actionInProgress = false;
        });

        $event.preventDefault();
        $event.stopPropagation();
    };

    $scope.deleteTask = function (task, $event) {
        var confirmed = confirm("Are you sure you want to delete " + task.content + "?");
        if (confirmed) {
            return checklistResource.deleteTask(task).then(function (result) {
                angular.forEach($scope.content.tasks, function (task, index) {
                    if (result.data.id === task.id) {
                        $scope.content.tasks.splice(index, 1);
                    }
                });
            });
        }

        $event.preventDefault();
        $event.stopPropagation();
    };
}

angular.module("umbraco").controller("checklist.contentAppController", ContentAppController);