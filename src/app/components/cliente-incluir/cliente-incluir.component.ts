import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cliente } from 'src/app/entities/cliente';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';
import { ClienteService } from 'src/app/services/cliente.service';

@Component({
  selector: 'app-cliente-incluir',
  templateUrl: './cliente-incluir.component.html',
  styleUrls: ['./cliente-incluir.component.css']
})
export class ClienteIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    nmCliente: ['', Validators.required],
    nmEmail: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
    dmSituacao: ['', Validators.required]
  })
  clienteService: any;
  
  
  constructor(private fornecedorService: ClienteService,
    public formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novoCliente: Cliente = {
        idCliente: 0,
        nmCliente: this.form.value.nmCliente,
        nmEmail: this.form.value.nmEmail,
        dmSituacao: Boolean(this.form.value.dmSituacao)
      }
      
      this.clienteService.incluir(novoCliente).subscribe((res: any) => {
        console.log(res)
        showSuccess("Cliente inserido")
      }, (error: any) => {
        if(error.status == 200) showSuccess('Cliente inserido')
        else console.log(error)
      })
    } else {
      showError("Dados inválidos!")
    }
  }
}
