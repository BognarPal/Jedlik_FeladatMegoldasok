<a href="https://code.visualstudio.com/">![Visual Studio Code](https://img.shields.io/static/v1?style=for-the-badge&message=Visual+Studio+Code&color=007ACC&logo=Visual+Studio+Code&logoColor=FFFFFF&label=)</a>
<a href="https://github.com/">![GitHub](https://img.shields.io/static/v1?style=for-the-badge&message=GitHub&color=181717&logo=GitHub&logoColor=FFFFFF&label=)</a>
<a href="https://nx.dev/">![Nx](https://img.shields.io/static/v1?style=for-the-badge&message=Nx&color=143055&logo=Nx&logoColor=FFFFFF&label=)</a>
<a href="https://www.mysql.com/">![MySQL](https://img.shields.io/static/v1?style=for-the-badge&message=MySQL&color=4479A1&logo=MySQL&logoColor=FFFFFF&label=)</a>
<a href="https://nestjs.com/">![NestJS](https://img.shields.io/static/v1?style=for-the-badge&message=NestJS&color=E0234E&logo=NestJS&logoColor=FFFFFF&label=)</a>
<a href="https://angular.io/">![Angular](https://img.shields.io/static/v1?style=for-the-badge&message=Angular&color=DD0031&logo=Angular&logoColor=FFFFFF&label=)</a>
<a href="https://meet.jit.si/">![Jitsi](https://img.shields.io/static/v1?style=for-the-badge&message=Jitsi&color=97979A&logo=Jitsi&logoColor=FFFFFF&label=)</a>
<a href="https://about.gitlab.com/">![GitLab](https://img.shields.io/static/v1?style=for-the-badge&message=GitLab&color=222222&logo=GitLab&logoColor=FCA121&label=)</a>
<a href="https://www.typescriptlang.org/">![TypeScript](https://img.shields.io/static/v1?style=for-the-badge&message=TypeScript&color=3178C6&logo=TypeScript&logoColor=FFFFFF&label=)</a>

# CatchMe
Ez itt a "Kapj el, ha tudsz." online társasjáték!

Könnyed szórakozás online videochat lehetőséggel. 

Kissebb dokumentáció: /catch-me/Kapj_El_Ha_Tudsz_-_Dokumentáció.odt

# Fejlesztőknek:

## Használat módja:
Az npm parancsokat a /catch-me azaz a root alatt futtasd! továbbiak->https://nx.dev/

Az API felé a kérésgyűjtemény megtalálható thunder-collection*.json névvel ellátva. továbbiak->https://www.thunderclient.com/
## Itt tudod online futtatni az appot gitpod segítségével:

[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://versenydonto.nisz.hu/csapat03/projekt03)
továbbiak-> https://www.gitpod.io/

### Az elindításhoz szükséges:

- `npm i`
- MySql adatbázis "catch-me-nanm" néven (CREATE DATABASE `catch-me-nanm`;)
továbbiak-> https://www.mysql.com/

### Indítás

kliens _(elérhető-> http://localhost:4200/)_:

- `nx serve catch-me`

szerver _(elérhető-> http://localhost:3333/)_:

- `nx serve api`

tesztelés (sajnos nem túl kiforrott):

- `nx e2e catch-me-e2e --watch` 
  <sub><sup>Ha probléma van a Cypress-el tekerj lejjebb avagy továbbiak-> https://www.cypress.io/</sup></sub>

### Build

- `nx run catch-me:build`
- `nx run-many --target=build --projects=catch-me,api`

### Más hasznos parancsok:

- Run `nx affected:apps` to get all apps which you have changed.
- Run `nx affected:libs` to get all libraries which you have changed.
- Run `nx affected:test` to test all apps which you have changed.
- Run `nx affected:test -- --only-failed` to test all apps which failed last time.
- Run `nx test catch-me` to test the 'catch-me' app.
- Run `ng g @nrwl/angular:lib new-lib` to generate the 'new-lib' library.
- Run `ng g component components/my-component --project=catch-me` to add 'my-component' component to the 'catch-me' project

Ha a fent említett módon nem működne a cypress:

- `npm cache clear -f`
- `npm i cypress -D`
- `node_modules/.bin/cypress install`

