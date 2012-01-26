/// <reference path="jquery-1.5.1.min.js" />

function changeImportance(systemName, projectId) {
    var divName = "#forImportance" + systemName;
    $(divName).load("/Project/ChangeImportance", { projectId: projectId, systemName: systemName });
}

