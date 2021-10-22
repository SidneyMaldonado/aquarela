import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cliente } from 'src/app/entities/cliente';
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
  
  
  constructor(private fornecedorService: ClienteService,
    public formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
  }

  showError(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("err")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }
  }

  showSuccess(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("success")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }
  }
  submit() {
    if(this.form.valid) {
      const novoCliente: Cliente = {
        idCliente: 0,
        nmCliente: this.form.value.nmCliente,
        nmEmail: this.form.value.nmEmail,
        dmSituacao: Boolean(this.form.value.dmSituacao)
      }
      
      this.fornecedorService.incluir(novoCliente).subscribe(res => {
        console.log(res)
        this.showSuccess("Cliente inserido")
      }, error => {
        if(error.status == 200) this.showSuccess('Cliente inserido')
        else console.log(error)
      })
    } else {
      this.showError("Dados inválidos!")
    }
  }
}
