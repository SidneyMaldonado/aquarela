import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TipoPagar } from 'src/app/entities/tipoPagar';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';
import { TipopagarService } from 'src/app/services/tipopagar.service';

@Component({
  selector: 'app-tipo-pagar-incluir',
  templateUrl: './tipo-pagar-incluir.component.html',
  styleUrls: ['./tipo-pagar-incluir.component.css']
})
export class TipoPagarIncluirComponent implements OnInit {

  form:FormGroup = this.formBuilder.group({
    nomeTipoConta:['', Validators.required], 
    nomeTipoPagar: ['', Validators.required],
    dmSituacao: ['', Validators.required]
  })
  constructor(private tipoPagarService: TipopagarService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }
  incluir(){
    if(!this.form.valid){
      showError("Dados inválidos.")
      return;
    }
    let body:TipoPagar = this.form.value
    body.dmSituacao = Boolean(body.dmSituacao)
    this.tipoPagarService.incluir(body).subscribe(
      data=>alert(data),
      err=>{
        console.log(err)
        if(err.status==200){
          showSuccess("Sucesso ao incluir tipo pagar!")
        }else{
          showError(err.message)
        }
      }
    )
  }

}
