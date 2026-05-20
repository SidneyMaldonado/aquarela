// Cripy.cpp : Funções de criptografia e descriptografia de strings
//

#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <iomanip>

// Função para criptografar uma string usando XOR com chave
std::string Criptografar(const std::string& texto, const std::string& chave = "AquarelaKey2026") {
    std::string resultado;
    size_t chaveLen = chave.length();

    for (size_t i = 0; i < texto.length(); i++) {
        // XOR de cada caractere com a chave (rotação circular da chave)
        char c = texto[i] ^ chave[i % chaveLen];
        resultado += c;
    }

    // Converter para hexadecimal para exibição
    std::stringstream ss;
    for (unsigned char c : resultado) {
        ss << std::hex << std::setw(2) << std::setfill('0') << (int)c;
    }

    return ss.str();
}

// Função para descriptografar uma string hexadecimal
std::string Descriptografar(const std::string& textoHex, const std::string& chave = "AquarelaKey2026") {
    // Converter hexadecimal de volta para bytes
    std::string textoCriptografado;
    for (size_t i = 0; i < textoHex.length(); i += 2) {
        std::string byteString = textoHex.substr(i, 2);
        char byte = (char)std::stoi(byteString, nullptr, 16);
        textoCriptografado += byte;
    }

    // Descriptografar usando XOR com a mesma chave
    std::string resultado;
    size_t chaveLen = chave.length();

    for (size_t i = 0; i < textoCriptografado.length(); i++) {
        char c = textoCriptografado[i] ^ chave[i % chaveLen];
        resultado += c;
    }

    return resultado;
}

int main()
{
    std::string opcao;
    std::string texto;

    std::cout << "=== Sistema de Criptografia ===" << std::endl;
    std::cout << "1 - Criptografar" << std::endl;
    std::cout << "2 - Descriptografar" << std::endl;
    std::cout << "Escolha uma opcao: ";
    std::getline(std::cin, opcao);

    if (opcao == "1") {
        std::cout << "\nDigite o texto para criptografar: ";
        std::getline(std::cin, texto);

        std::string textoCriptografado = Criptografar(texto);

        std::cout << "\n=== RESULTADO ===" << std::endl;
        std::cout << "Texto Original: " << texto << std::endl;
        std::cout << "Texto Criptografado (Hex): " << textoCriptografado << std::endl;
    }
    else if (opcao == "2") {
        std::cout << "\nDigite o texto criptografado (em hexadecimal): ";
        std::getline(std::cin, texto);

        try {
            std::string textoDescriptografado = Descriptografar(texto);

            std::cout << "\n=== RESULTADO ===" << std::endl;
            std::cout << "Texto Criptografado (Hex): " << texto << std::endl;
            std::cout << "Texto Descriptografado: " << textoDescriptografado << std::endl;
        }
        catch (const std::exception& e) {
            std::cout << "Erro ao descriptografar: " << e.what() << std::endl;
        }
    }
    else {
        std::cout << "Opcao invalida!" << std::endl;
    }

    std::cout << "\nPressione Enter para sair...";
    std::cin.get();

    return 0;
}

