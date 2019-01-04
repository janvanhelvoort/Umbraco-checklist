function ChecklistResource($http) {
    return {
        // Tasks
        getTasks: function (contentNodeId) {
            return $http.get(Umbraco.Sys.ServerVariables.checklist.getTasks + "?contentNodeId=" + contentNodeId);
        },
        saveTask: function (task) {
            return $http.post(Umbraco.Sys.ServerVariables.checklist.saveTask, task);
        },
        deleteTask: function (task) {
            return $http.delete(Umbraco.Sys.ServerVariables.checklist.deleteTask + "?id=" + task.id);
        }
    };
};

angular.module("umbraco.resources").factory("checklistResource", ChecklistResource);