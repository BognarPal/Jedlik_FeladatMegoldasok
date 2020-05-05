import { Injectable, EventEmitter } from '@angular/core';
import { Observable, Observer } from 'rxjs';
declare var gapi: any;

@Injectable({
  providedIn: 'root'
})
export class GoogleApiService {

  public apiLoadedChange = new EventEmitter();
  public apiLoaded = false;

  constructor() {
    // console.log('Loading Google API');
    this.loadApiJs().subscribe(
      (success: boolean) => {
        this.apiLoaded = success;
        this.apiLoadedChange.emit(success);
      }
    );

  }

  private loadApiJs(): Observable<boolean> {
    return new Observable((observer: Observer<boolean>) => {
      const node = document.createElement('script');
      node.src = 'https://apis.google.com/js/api.js';
      node.type = 'text/javascript';
      node.async = true;
      node.defer = true;
      document.getElementsByTagName('body')[0].appendChild(node);
      node.onload = () => {
        observer.next(true);
        observer.complete();
      };
    });
  }

  public groupMembersEmail(group: any): Promise<string[]> {
    let emails = [];
    return this.listGroupMembers(group.email, null).then(async (result) => {
      if (result) {
        result.filter(r => r.type === 'USER').forEach(r => emails.push(r.email));

        // sajos csak sorosan tudtam megoldani...
        for (const g of result.filter(r => r.type === 'GROUP')) {
          emails = emails.concat(await this.groupMembersEmail(g));
        }
        // elilveg ez lenne a párhuzamos feldolgozás, de nem működik, nem ad értéket az emails változónak
        /*await Promise.all(result.filter(r => r.type === 'GROUP').map( async (g) => {
          emails = emails.concat(await this.listMembersEmail(g));
        }));*/
        return emails;
      }
    });
  }

  private listGroupMembers(email: string, token: any): Promise<any[]> {
    return gapi.client.directory.members.list({
      groupKey: email,
      maxResults: 200,
      pageToken: token
    }).then((response: any) => {
      // console.log(response.result);
      if (response.result.nextPageToken) {
        return this.listGroupMembers(email, response.result.nextPageToken
        ).then((members) => {
          return response.result.members.concat(members);
        });
      } else {
        return response.result.members;
      }
    });
  }
}
