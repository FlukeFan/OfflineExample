
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\StorageService.js"/>
/// <reference path="..\Dto\CmdFetchFuture.js"/>
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>

describe("StorageService tests", function () {

    it("should query server for new list when onExecuteCommands is raised", function () {
        var localStore = new localStorageStub();
        var service = new StorageService(localStore);

        var calledCommand;
        var results = [1, 2, 3];

        service.execute = function (cmd, callback) {
            calledCommand = cmd;
            callback(results);
        };

        OfflineGlobal.onExecuteCommands();

        expect(calledCommand.toJson()).toBe(new CmdFetchFuture().toJson());
        expect(localStore[StorageService.KEY_APPPOINTMENTS]).toBe(JSON.stringify(results));
    });

});
