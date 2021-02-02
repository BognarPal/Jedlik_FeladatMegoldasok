(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./$$_lazy_route_resource lazy recursive":
/*!******************************************************!*\
  !*** ./$$_lazy_route_resource lazy namespace object ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./node_modules/raw-loader/index.js!./src/app/app.component.html":
/*!**************************************************************!*\
  !*** ./node_modules/raw-loader!./src/app/app.component.html ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"page\" fxLayout=\"column\" fxLayoutAlign=\"center center\">\n  <div class=\"container\">\n    <div style=\"text-align:center\">\n      <h1>\n        Oldtimer Veteránautó-kölcsönző - Adminisztrációs oldal\n      </h1>\n      <div fxLayout=\"row\">\n        <p>Ezzel a gombbal újratöltheti a lekérdezéseket:</p>\n        <button mat-raised-button color='primary' (click)=\"refresh()\">\n          Adatok frissítése\n          <mat-icon>refresh</mat-icon>\n        </button>\n      </div>\n\n    </div>\n\n    <h2>Szerver státusz:</h2>\n    <mat-card>\n      <p>\n        Az alábbi gombok jelzik, hogy rendben működik-e az Alkalmazásszerver és az Adatbázis:\n      </p>\n      <table>\n        <tr>\n          <td>Alkalmazásserver státusz:</td>\n          <td>\n            <button mat-button\n                    [ngClass]=\"{'pending-button':serverStatus.indicator === 'pending',\n              'offline-button':serverStatus.indicator === 'offline',\n              'active-button':serverStatus.indicator === 'active'}\">\n              <ng-container [ngSwitch]=\"serverStatus.indicator\">\n                <ng-container *ngSwitchCase=\"'pending'\">Lekérdezés...</ng-container>\n                <ng-container *ngSwitchCase=\"'offline'\">Nem elérhető!</ng-container>\n                <ng-container *ngSwitchDefault>Elérhető</ng-container>\n              </ng-container>\n            </button>\n          </td>\n        </tr>\n        <tr>\n          <td>MySQL adatbázis státusz:</td>\n          <td>\n            <button mat-button [ngClass]=\"{'pending-button':mysqlStatus.indicator === 'pending',\n              'offline-button':mysqlStatus.indicator === 'offline',\n              'active-button':mysqlStatus.indicator === 'active'}\">\n              <ng-container [ngSwitch]=\"mysqlStatus.indicator\">\n                <ng-container *ngSwitchCase=\"'pending'\">Lekérdezés...</ng-container>\n                <ng-container *ngSwitchCase=\"'offline'\">Nem elérhető!</ng-container>\n                <ng-container *ngSwitchDefault>Elérhető</ng-container>\n                <ng-container *ngIf=\"dbIsNotYetCreated\"> Az adatbázist még nem hozták létre.\n                </ng-container>\n              </ng-container>\n            </button>\n          </td>\n        </tr>\n      </table>\n    </mat-card>\n\n    <h2>SQL lekérdezések eredményei:</h2>\n    <ng-container *ngIf=\"sqlTasks; else noEntry\">\n      <ng-container *ngFor=\"let entry of sqlTasks\">\n\n        <mat-card>\n          <mat-card-header>\n            <h2>{{entry.id}} Feladat:</h2>\n          </mat-card-header>\n\n          <h4 class=\"sql-description\">{{entry.description}}</h4>\n          <ng-container *ngIf=\"entry.response; else waitingForEntries\">\n            <table class=\"query-table\">\n              <ng-container *ngIf=\"entry.response.empty; else resultTemplate\">\n                <p><b>Ehhez a feladathoz még nem szerepel SQL lekérdezés a beadandó fájlban\n                  (/lekerdezesek/lekerdezesek.sql).</b></p>\n              </ng-container>\n              <ng-template #resultTemplate>\n                <tr>\n                  <th *ngFor=\"let key of Object.keys(entry?.response[0])\">{{key}}</th>\n                </tr>\n                <ng-container *ngFor=\"let resultEntry of entry.response\">\n\n                  <tr>\n                    <td *ngFor=\"let val of Object.values(resultEntry)\">{{val}}</td>\n                  </tr>\n                </ng-container>\n              </ng-template>\n            </table>\n          </ng-container>\n\n        </mat-card>\n\n\n        <mat-divider></mat-divider>\n      </ng-container>\n    </ng-container>\n    <h2>Felhasználói vélemények</h2>\n    <button mat-raised-button color='warn' (click)=\"deleteVelemenyek()\">Vélemények törlése a szerveren\n      <mat-icon>delete</mat-icon>\n    </button>\n    <button mat-raised-button color='primary' (click)=\"reloadVelemenyek()\">\n      Vélemények frissítése\n      <mat-icon>refresh</mat-icon>\n    </button>\n    <mat-card *ngIf=\"serverStatus.indicator === 'active'; else noServerForVelemenyek\">\n      <ng-container *ngIf=\"velemenyek; else waitingForVelemenyek\">\n        <table class=\"query-table\">\n          <tr>\n            <th>Bejegyzés</th>\n            <th>Dátum</th>\n          </tr>\n          <ng-container *ngFor=\"let entry of velemenyek\">\n            <tr>\n              <td *ngFor=\"let val of Object.values(entry)\">{{val}}</td>\n            </tr>\n          </ng-container>\n        </table>\n      </ng-container>\n    </mat-card>\n\n    <ng-template #noServerForVelemenyek>\n      <mat-card>\n        <mat-card-header>Az alkalamzás szervernek futnia kell a vélemények betöltéséhez.</mat-card-header>\n      </mat-card>\n    </ng-template>\n\n    <ng-template #noEntry>\n      Nem sikerült az SQL lekérdezéseket lekérdezni.\n    </ng-template>\n\n    <ng-template #waitingForEntries>\n      <p>A lekérdezéshez tartozó válasz betöltése nem sikerült.</p>\n    </ng-template>\n\n    <ng-template #waitingForVelemenyek>\n      <p>A Vélemény bejegyzéseket nem sikerült lekérdezni.</p>\n    </ng-template>\n\n  </div>\n</div>\n\n"

/***/ }),

/***/ "./src/app/app.component.scss":
/*!************************************!*\
  !*** ./src/app/app.component.scss ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".pending-button {\n  background-color: yellow !important;\n}\n\n.offline-button {\n  background-color: red !important;\n}\n\n.active-button {\n  background-color: green !important;\n}\n\n.container {\n  font-family: \"Trebuchet MS\", Arial, Helvetica, sans-serif;\n  background-color: #d3eadf;\n  padding: 10px;\n}\n\n.query-table {\n  border-collapse: collapse;\n  width: 100%;\n}\n\n.query-table td, .query-table th {\n  border: 1px solid #ddd;\n  padding: 8px;\n}\n\n.query-table tr:nth-child(even) {\n  background-color: #f2f2f2;\n}\n\n.query-table tr:hover {\n  background-color: #ddd;\n}\n\n.query-table th {\n  padding-top: 12px;\n  padding-bottom: 12px;\n  text-align: left;\n  background-color: #4CAF50;\n  color: white;\n}\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvQzpcXFVzZXJzXFxvcmFjbGVcXERlc2t0b3BcXEZvcmVzdFxcMS43XFxhZG1pbi1sZWbDvGxldC1zb3VyY2Uta8OzZGphXFxhZG1pbi9zcmNcXGFwcFxcYXBwLmNvbXBvbmVudC5zY3NzIiwic3JjL2FwcC9hcHAuY29tcG9uZW50LnNjc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxtQ0FBQTtBQ0NGOztBREVBO0VBQ0UsZ0NBQUE7QUNDRjs7QURFQTtFQUNFLGtDQUFBO0FDQ0Y7O0FERUE7RUFDRSx5REFBQTtFQUNBLHlCQUFBO0VBQ0EsYUFBQTtBQ0NGOztBREVBO0VBQ0UseUJBQUE7RUFDQSxXQUFBO0FDQ0Y7O0FEQ0U7RUFDRSxzQkFBQTtFQUNBLFlBQUE7QUNDSjs7QURFRTtFQUNFLHlCQUFBO0FDQUo7O0FER0U7RUFDRSxzQkFBQTtBQ0RKOztBRElFO0VBQ0UsaUJBQUE7RUFDQSxvQkFBQTtFQUNBLGdCQUFBO0VBQ0EseUJBQUE7RUFDQSxZQUFBO0FDRkoiLCJmaWxlIjoic3JjL2FwcC9hcHAuY29tcG9uZW50LnNjc3MiLCJzb3VyY2VzQ29udGVudCI6WyIucGVuZGluZy1idXR0b24ge1xyXG4gIGJhY2tncm91bmQtY29sb3I6IHllbGxvdyAhaW1wb3J0YW50O1xyXG59XHJcblxyXG4ub2ZmbGluZS1idXR0b24ge1xyXG4gIGJhY2tncm91bmQtY29sb3I6IHJlZCAhaW1wb3J0YW50O1xyXG59XHJcblxyXG4uYWN0aXZlLWJ1dHRvbiB7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogZ3JlZW4gIWltcG9ydGFudDtcclxufVxyXG5cclxuLmNvbnRhaW5lciB7XHJcbiAgZm9udC1mYW1pbHk6IFwiVHJlYnVjaGV0IE1TXCIsIEFyaWFsLCBIZWx2ZXRpY2EsIHNhbnMtc2VyaWY7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogI2QzZWFkZjtcclxuICBwYWRkaW5nOiAxMHB4O1xyXG59XHJcblxyXG4ucXVlcnktdGFibGUge1xyXG4gIGJvcmRlci1jb2xsYXBzZTogY29sbGFwc2U7XHJcbiAgd2lkdGg6IDEwMCU7XHJcblxyXG4gIHRkLCB0aCB7XHJcbiAgICBib3JkZXI6IDFweCBzb2xpZCAjZGRkO1xyXG4gICAgcGFkZGluZzogOHB4O1xyXG4gIH1cclxuXHJcbiAgdHI6bnRoLWNoaWxkKGV2ZW4pIHtcclxuICAgIGJhY2tncm91bmQtY29sb3I6ICNmMmYyZjI7XHJcbiAgfVxyXG5cclxuICB0cjpob3ZlciB7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiAjZGRkO1xyXG4gIH1cclxuXHJcbiAgdGgge1xyXG4gICAgcGFkZGluZy10b3A6IDEycHg7XHJcbiAgICBwYWRkaW5nLWJvdHRvbTogMTJweDtcclxuICAgIHRleHQtYWxpZ246IGxlZnQ7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiAjNENBRjUwO1xyXG4gICAgY29sb3I6IHdoaXRlO1xyXG4gIH1cclxufVxyXG4iLCIucGVuZGluZy1idXR0b24ge1xuICBiYWNrZ3JvdW5kLWNvbG9yOiB5ZWxsb3cgIWltcG9ydGFudDtcbn1cblxuLm9mZmxpbmUtYnV0dG9uIHtcbiAgYmFja2dyb3VuZC1jb2xvcjogcmVkICFpbXBvcnRhbnQ7XG59XG5cbi5hY3RpdmUtYnV0dG9uIHtcbiAgYmFja2dyb3VuZC1jb2xvcjogZ3JlZW4gIWltcG9ydGFudDtcbn1cblxuLmNvbnRhaW5lciB7XG4gIGZvbnQtZmFtaWx5OiBcIlRyZWJ1Y2hldCBNU1wiLCBBcmlhbCwgSGVsdmV0aWNhLCBzYW5zLXNlcmlmO1xuICBiYWNrZ3JvdW5kLWNvbG9yOiAjZDNlYWRmO1xuICBwYWRkaW5nOiAxMHB4O1xufVxuXG4ucXVlcnktdGFibGUge1xuICBib3JkZXItY29sbGFwc2U6IGNvbGxhcHNlO1xuICB3aWR0aDogMTAwJTtcbn1cbi5xdWVyeS10YWJsZSB0ZCwgLnF1ZXJ5LXRhYmxlIHRoIHtcbiAgYm9yZGVyOiAxcHggc29saWQgI2RkZDtcbiAgcGFkZGluZzogOHB4O1xufVxuLnF1ZXJ5LXRhYmxlIHRyOm50aC1jaGlsZChldmVuKSB7XG4gIGJhY2tncm91bmQtY29sb3I6ICNmMmYyZjI7XG59XG4ucXVlcnktdGFibGUgdHI6aG92ZXIge1xuICBiYWNrZ3JvdW5kLWNvbG9yOiAjZGRkO1xufVxuLnF1ZXJ5LXRhYmxlIHRoIHtcbiAgcGFkZGluZy10b3A6IDEycHg7XG4gIHBhZGRpbmctYm90dG9tOiAxMnB4O1xuICB0ZXh0LWFsaWduOiBsZWZ0O1xuICBiYWNrZ3JvdW5kLWNvbG9yOiAjNENBRjUwO1xuICBjb2xvcjogd2hpdGU7XG59Il19 */"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../environments/environment */ "./src/environments/environment.ts");




var AppComponent = /** @class */ (function () {
    function AppComponent(http) {
        this.http = http;
        this.sqlTaskSubscriptions = {};
        this.velemenyek = null;
        this.dbIsNotYetCreated = false;
        this.Object = Object; // a template fájlokban is elérhető így
        this.serverStatus = {
            subscription: null,
            interval: null,
            indicator: StatusIndicator.pending,
            url: '/serverStatus'
        };
        this.mysqlStatus = {
            subscription: null,
            interval: null,
            indicator: StatusIndicator.pending,
            url: '/mysqlStatus'
        };
    }
    AppComponent.prototype.ngOnInit = function () {
        this.reloadSqlTasks();
        this.startMonitoring();
        this.reloadVelemenyek();
    };
    AppComponent.prototype.ngOnDestroy = function () {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
        if (this.serverStatus.subscription) {
            this.serverStatus.subscription.unsubscribe();
        }
        if (this.mysqlStatus.subscription) {
            this.mysqlStatus.subscription.unsubscribe();
        }
        if (this.serverStatus.interval) {
            clearInterval(this.serverStatus.interval);
        }
        if (this.mysqlStatus.interval) {
            clearInterval(this.mysqlStatus.interval);
        }
    };
    AppComponent.prototype.startMonitoring = function () {
        this.startMonitoringInterval(this.serverStatus);
        this.startMonitoringInterval(this.mysqlStatus);
    };
    AppComponent.prototype.startMonitoringInterval = function (statusObj) {
        var _this = this;
        this.createHttpRequest(statusObj);
        statusObj.interval = setInterval(function () {
            _this.createRESTSubscription(statusObj);
        }, 5000);
    };
    AppComponent.prototype.createRESTSubscription = function (statusObj) {
        if (statusObj.subscription) {
            statusObj.subscription.unsubscribe();
        }
        statusObj.subscription = this.createHttpRequest(statusObj);
    };
    AppComponent.prototype.createHttpRequest = function (statusObj) {
        return this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].baseUrl + statusObj.url).subscribe(function (res) {
            if (res && res.alive) {
                statusObj.indicator = StatusIndicator.active;
            }
            else {
                statusObj.indicator = StatusIndicator.offline;
            }
        }, function (err) {
            console.error('Error with ', statusObj.url, ' status:', err);
            statusObj.indicator = StatusIndicator.offline;
        });
    };
    AppComponent.prototype.reloadSqlTasks = function () {
        var _this = this;
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
        this.subscription = this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].baseUrl + '/sqltasks').subscribe(function (res) {
            _this.sqlTasks = res.filter(function (task) { return task.adminPage; });
            _this.requestSqlTaskResults();
        }, function (err) {
            console.log('err: ', err);
        });
    };
    AppComponent.prototype.reloadVelemenyek = function () {
        var _this = this;
        this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].baseUrl + '/velemeny').subscribe(function (res) {
            _this.velemenyek = res;
        }, function (err) {
            console.log('err: ', err);
        });
    };
    AppComponent.prototype.deleteVelemenyek = function () {
        var _this = this;
        this.http.delete(_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].baseUrl + '/velemeny').subscribe(function (res) {
            console.log(res.message);
            _this.velemenyek = [];
        }, function (err) {
            console.log('err: ', err);
            _this.reloadVelemenyek();
        });
    };
    AppComponent.prototype.requestSqlTaskResults = function () {
        var _this = this;
        this.sqlTasks.forEach(function (entry) {
            if (_this.sqlTaskSubscriptions[entry.id]) {
                _this.sqlTaskSubscriptions[entry.id].unsubscribe();
            }
            _this.sqlTaskSubscriptions[entry.id] = _this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].baseUrl + '/lekerdezes/' + entry.id).subscribe(function (res) {
                _this.dbIsNotYetCreated = false;
                entry.response = res;
            }, function (err) {
                if (err.error && err.error.error && err.error.error === 'ER_BAD_DB_ERROR') {
                    _this.dbIsNotYetCreated = true;
                }
            });
        });
    };
    AppComponent.prototype.refresh = function () {
        this.serverStatus.indicator = StatusIndicator.pending;
        this.mysqlStatus.indicator = StatusIndicator.pending;
        this.ngOnDestroy();
        this.ngOnInit();
    };
    AppComponent.ctorParameters = function () { return [
        { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"] }
    ]; };
    AppComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! raw-loader!./app.component.html */ "./node_modules/raw-loader/index.js!./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.scss */ "./src/app/app.component.scss")]
        })
    ], AppComponent);
    return AppComponent;
}());

var StatusIndicator;
(function (StatusIndicator) {
    StatusIndicator["pending"] = "pending";
    StatusIndicator["active"] = "active";
    StatusIndicator["offline"] = "offline";
})(StatusIndicator || (StatusIndicator = {}));


/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/platform-browser/animations */ "./node_modules/@angular/platform-browser/fesm5/animations.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _angular_flex_layout__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/flex-layout */ "./node_modules/@angular/flex-layout/esm5/flex-layout.es5.js");








var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]
            ],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"],
                _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_5__["BrowserAnimationsModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatCardModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatDividerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatListModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClientModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatButtonModule"],
                _angular_flex_layout__WEBPACK_IMPORTED_MODULE_7__["FlexLayoutModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_6__["MatIconModule"]
            ],
            providers: [],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
var environment = {
    production: false,
    baseUrl: 'http://localhost:8000/api'
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.error(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\Users\oracle\Desktop\Forest\1.7\admin-lefület-source-kódja\admin\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main-es5.js.map