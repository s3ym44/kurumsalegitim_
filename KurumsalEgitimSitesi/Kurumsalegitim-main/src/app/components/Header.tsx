import { ChevronDown, User } from 'lucide-react';

export function Header() {
  return (
    <header className="bg-[#1a2e4a] text-white py-4">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between">
          {/* Logo */}
          <div className="flex items-center gap-3">
            <div className="bg-gradient-to-br from-blue-400 to-teal-500 p-2 rounded">
              <div className="w-6 h-6 flex items-center justify-center">
                <div className="w-3 h-3 bg-white transform rotate-45"></div>
              </div>
            </div>
            <div>
              <div className="text-lg font-semibold">GorTepe</div>
              <div className="text-xs text-gray-300">Müşavirlik Hizm.</div>
            </div>
          </div>

          {/* Navigation */}
          <nav className="hidden lg:flex items-center gap-6 text-sm">
            <a href="#" className="hover:text-orange-400 transition-colors flex items-center gap-1">
              Eğitimler <ChevronDown className="w-4 h-4" />
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors">
              Takvim
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors">
              Kişisel Gelişim
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors">
              Tesisimiz
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors">
              İletişim
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors flex items-center gap-1">
              Seminerler <ChevronDown className="w-4 h-4" />
            </a>
            <a href="#" className="hover:text-orange-400 transition-colors flex items-center gap-1">
              Eğitmenler <ChevronDown className="w-4 h-4" />
            </a>
          </nav>

          {/* Auth Buttons */}
          <div className="flex items-center gap-3">
            <button className="bg-orange-500 hover:bg-orange-600 px-5 py-2 rounded text-sm transition-colors">
              Takvim
            </button>
            <button className="border border-white/30 hover:bg-white/10 px-5 py-2 rounded text-sm flex items-center gap-2 transition-colors">
              <User className="w-4 h-4" />
              Kayıt Ol
            </button>
          </div>
        </div>
      </div>
    </header>
  );
}
