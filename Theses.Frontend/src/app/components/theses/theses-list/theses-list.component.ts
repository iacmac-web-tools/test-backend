import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-theses-list',
  templateUrl: './theses-list.component.html',
  styleUrls: ['./theses-list.component.css']
})
export class ThesesListComponent {
  content: string = '';
  mainAuthor?: string;
  contactEmail: string = '';
  topic: string = '';

  constructor(private http: HttpClient) { }

  onSendClick() {
    const newThesis = {
      id: 99,
      mainAuthor: this.mainAuthor,
      contactEmail: this.contactEmail,
      topic: this.topic,
      content: this.content
    };

    this.http.post<any>('URL_вашего_API', newThesis).subscribe(
      response => {
        console.log(response);
      },
      error => {
        console.error(error);
      }
    );
  }
}
