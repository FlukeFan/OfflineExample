
/// <reference path="..\..\spec\GlobalStubs.js"/>
/// <reference path="..\StorageService.js"/>
/// <reference path="..\Dto\Appointment.js"/>
/// <reference path="..\Dto\CmdFetchFuture.js"/>
/// <reference path="..\..\..\..\..\SDKs\jasmine\jasmine.js"/>

describe("StorageService tests", function () {

    it("should query server for new list when onExecuteCommands is raised", function () {
        // arrange
        var localStore = new localStorageStub();
        var service = new StorageService(localStore);

        var calledCommand;
        var results = [1, 2, 3];

        service.execute = function (cmd, callback) {
            calledCommand = cmd;
            callback(results);
        };

        // act
        OfflineGlobal.onExecuteCommands();

        // assert
        expect(calledCommand.toJson()).toBe(new CmdFetchFuture().toJson());
        expect(localStore[StorageService.KEY_APPPOINTMENTS]).toBe(JSON.stringify(results));
    });

    it("should return the stored list, grouped by month", function () {
        // arrange
        var localStore = new localStorageStub();
        var service = new StorageService(localStore);

        var storedAppointments = [
            new Appointment().setVisitDate(new Date("01 Jan 2001")).getRaw(),
            new Appointment().setVisitDate(new Date("02 Jan 2001")).getRaw(),
            new Appointment().setVisitDate(new Date("03 Mar 2001")).getRaw()
        ];

        localStore.setItem(StorageService.KEY_APPPOINTMENTS, JSON.stringify(storedAppointments));

        // act
        var result = service.getAllByMonth();

        // assert
        expect(result.length).toBe(2);

        expect(result[0].getMonth().valueOf()).toBe(new Date("01 Jan 2001").valueOf());
        expect(result[0].getCount()).toBe(2);

        expect(result[1].getMonth().valueOf()).toBe(new Date("01 Mar 2001").valueOf());
        expect(result[1].getCount()).toBe(1);
    });

    it("should return an empty list when nothing is stored", function () {
        // arrange
        var localStore = new localStorageStub();
        var service = new StorageService(localStore);

        // act
        var result = service.getAllByMonth();

        // assert
        expect(result.length).toBe(0);
    });

});
