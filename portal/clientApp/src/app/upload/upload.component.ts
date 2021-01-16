import { Component, OnInit } from '@angular/core';
import { UploadService } from '../upload.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

  constructor(private uploadService: UploadService) { }

  ngOnInit(): void {

  }

  getListFromServer() {
    this.uploadService.getList()
      .subscribe(list => {
        console.log(list);
      });
  }

}
