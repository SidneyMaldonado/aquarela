import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Produto } from 'src/app/entities/produto';
import { ProdutoService } from 'src/app/services/produto.service';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';

@Component({
  selector: 'app-produto-incluir',
  templateUrl: './produto-incluir.component.html',
  styleUrls: ['./produto-incluir.component.css']
})
export class ProdutoIncluirComponent implements OnInit {
  
  form: FormGroup = this.formBuilder.group({
    
    dsDescricao: ['', Validators.required],
    dsProduto: ['', Validators.required],
    foto: ['', Validators.required],
    dsBarCode: ['', Validators.required],
    nrPrecoCusto: ['', Validators.required],
    nrPrecoVenda: ['', Validators.required],
    nrQuantidade: ['', Validators.required],
    qrCode: ['', Validators.required],
    nrQuantidadeDesconto: ['', Validators.required],
    nrPrecoVendaDesconto: ['', Validators.required],
    dmSituacao: ['', Validators.required],
  })
  
  
  constructor(private produtoService: ProdutoService,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novoProduto: Produto = {
        idProduto: 0,
        dsDescricao: this.form.value.dsDescricao,
        dsProduto: this.form.value.dsProduto,
        foto: this.form.value.foto,
        dsBarCode: this.form.value.dsBarCode,
        nrPrecoCusto: this.form.value.nrPrecoCusto,
        nrPrecoVenda: this.form.value.nrPrecoVenda,
        nrQuantidade: this.form.value.nrQuantidade,
        qrCode: this.form.value.qrCode,
        nrQuantidadeDesconto: this.form.value.nrQuantidadeDesconto,
        nrPrecoVendaDesconto: this.form.value.nrPrecoVendaDesconto,
        dmSituacao: Boolean(this.form.value.dmSituacao)
      }

      this.produtoService.incluir(novoProduto).subscribe(res => {
        console.log(res)
        showSuccess("Produto inserido")
      }, error => {
        if(error.status == 200) showSuccess('Produto inserido')
        else console.log(error)
      })
    } else {
      showError("Dados inválidos!")
    }
  }
}
