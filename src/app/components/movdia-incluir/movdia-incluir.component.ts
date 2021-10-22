import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovDia } from 'src/app/entities/movdia';
import { MovdiaService } from 'src/app/services/movdia.service';
import { showError, showSuccess } from 'src/app/formHandler/formhandler'
@Component({
  selector: 'app-movdia-incluir',
  templateUrl: './movdia-incluir.component.html',
  styleUrls: ['./movdia-incluir.component.css']
})
export class MovdiaIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    dtMovDia:['', Validators.required],
    dmAtivo:['', Validators.required],
    vlNota100:['', Validators.required],
    vlNota50:['', Validators.required],
    vlNota20:['', Validators.required],
    vlNota10:['', Validators.required],
    vlNota5:['', Validators.required],
    vlNota2:['', Validators.required],
    vlPagseguro:['', Validators.required],
    vlDespesa:['', Validators.required],
    vlTroco:['', Validators.required],
    vlDeposito:['', Validators.required]
  })

  constructor(private movdiaService:MovdiaService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  incluir(){
    if(!this.form.valid){
      showError("Dados inválidos");
      return;
    }
    let {dtMovDia, dmAtivo, vlNota100, vlNota50, vlNota20, vlNota10, vlNota5, vlNota2, vlPagseguro, vlDespesa, vlDeposito, vlTroco}= this.form.value
    dmAtivo = Boolean(dmAtivo)
    this.movdiaService.incluir({dtMovDia, dmAtivo, vlNota100, vlNota50, vlNota20, vlNota10, vlNota5, vlNota2, vlPagseguro, vlDespesa, vlTroco, vlDeposito}).subscribe(
      response => alert(response),
      error =>{if(error.status==200){showSuccess("Sucesso ao incluir!")}}
    )
  }

}
