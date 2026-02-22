import { useState } from 'react';
import { Header } from './components/Header';
import { Hero } from './components/Hero';
import { ServiceCard } from './components/ServiceCard';
import { SeminarCard } from './components/SeminarCard';
import { TrainingCard } from './components/TrainingCard';
import { Scale, Monitor, Lightbulb, Star, ChevronRight, Mail, Phone, MapPin, Facebook, Twitter, Linkedin, Instagram } from 'lucide-react';

export default function App() {
  const [activeTab, setActiveTab] = useState('Tüm');

  const tabs = ['Tüm', 'Kamu', 'Teknoloji', 'Eğişel Gelişim', 'Özel Sektör'];

  const trainings = [
    {
      title: 'Etkili İletişim ve İkna Teknikleri',
      imageUrl: 'https://images.unsplash.com/photo-1759310610552-914069ec2e0b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxidXNpbmVzcyUyMHByb2Zlc3Npb25hbHMlMjBtZWV0aW5nJTIwY29uZmVyZW5jZXxlbnwxfHx8fDE3NzEyNzE5MzN8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral',
      hours: '4s. 16sa.',
      participants: '2.4Bin',
      lessons: '6-7 Mart',
      category: 'İletişim',
      level: 'Temel',
      location: 'İstanbul'
    },
    {
      title: 'Power BI İle Raporlama',
      imageUrl: 'https://images.unsplash.com/photo-1770681381576-f1fdceb2ea01?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxsYXB0b3AlMjBwcmVzZW50YXRpb24lMjBzY3JlZW4lMjBidXNpbmVzc3xlbnwxfHx8fDE3NzEyNzE5MzR8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral',
      hours: '1s. Online',
      participants: '2 Haf',
      lessons: '30 İstira',
      category: 'Performans',
      level: 'İleri',
      location: 'Yazılım'
    },
    {
      title: 'Siber Güvenlik Temel Eğitimi',
      imageUrl: 'https://images.unsplash.com/photo-1758876203511-34918d1a310b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxidXNpbmVzc21hbiUyMHRlY2hub2xvZ3klMjBtb2Rlcm4lMjBvZmZpY2V8ZW58MXx8fHwxNzcxMjcxOTMzfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral',
      hours: '½s. 9saat',
      participants: 'Sınırsız',
      lessons: '3 Gün/Haf',
      category: '26Mar/24',
      level: 'İleri',
      location: 'Mersin'
    },
    {
      title: 'Belediyelerde Dijital Dönüşüm',
      imageUrl: 'https://images.unsplash.com/photo-1758518727077-ffb66ffccced?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxidXNpbmVzcyUyMHBlb3BsZSUyMGRpc2N1c3Npb24lMjBvZmZpY2V8ZW58MXx8fHwxNzcxMjI2MzI1fDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral',
      hours: 'Herbarte',
      participants: '3 Haf',
      lessons: '9 Gün/Haf',
      category: 'İstanbul',
      level: 'Temel',
      location: 'Mersin'
    }
  ];

  return (
    <div className="min-h-screen bg-gray-50">
      <Header />
      
      <Hero imageUrl="https://images.unsplash.com/photo-1681949222860-9cb3b0329878?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxwcm9mZXNzaW9uYWwlMjBidXNpbmVzc3dvbWFuJTIwcHJlc2VudGluZyUyMHdoaXRlYm9hcmQlMjBvZmZpY2V8ZW58MXx8fHwxNzcxMjcxOTMxfDA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral" />

      {/* Service Cards */}
      <section className="container mx-auto px-4 py-16 relative z-10">
        <div className="grid md:grid-cols-3 gap-6">
          <ServiceCard
            icon={Scale}
            title="Mevzuat & Hukuk"
            description="İhale, Kamu & Belediye Mevzuatı"
            buttonColor="bg-orange-500"
            bgColor="bg-orange-50"
          />
          <ServiceCard
            icon={Monitor}
            title="Teknoloji & Dijital Dönüşüm"
            description="Yapay Zeka, Zen, Ses, Maaretlzm"
            buttonColor="bg-blue-600"
            bgColor="bg-blue-50"
          />
          <ServiceCard
            icon={Lightbulb}
            title="Kişisel Gelişim"
            description="Liderlik, Etkili İletişim, Zaman Yönetimi"
            buttonColor="bg-orange-500"
            bgColor="bg-orange-50"
          />
        </div>
      </section>

      {/* Antalya Seminars Section */}
      <section 
        className="relative bg-cover bg-center py-16"
        style={{ 
          backgroundImage: `linear-gradient(rgba(100, 116, 139, 0.85), rgba(100, 116, 139, 0.85)), url(https://images.unsplash.com/photo-1730825109888-c13511e22500?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxsdXh1cnklMjByZXNvcnQlMjBob3RlbCUyMHBvb2wlMjBhbnRhbHlhJTIwdHVya2V5fGVufDF8fHx8MTc3MTI3MTkzMnww&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral)` 
        }}
      >
        <div className="container mx-auto px-4">
          <div className="flex items-start justify-between mb-12">
            <div>
              <h2 className="text-white text-3xl lg:text-4xl font-bold mb-4">
                Antalya'da Mevzuat ve İhale
                <br />
                Uygulamaları Seminerleri
              </h2>
              <div className="flex gap-1">
                {[1, 2, 3, 4, 5].map((star) => (
                  <Star key={star} className="w-6 h-6 text-yellow-400 fill-yellow-400" />
                ))}
              </div>
            </div>
            <div className="hidden lg:block">
              <div className="bg-gradient-to-br from-yellow-400 to-yellow-600 rounded-full p-8 relative">
                <div className="absolute inset-0 flex flex-col items-center justify-center">
                  <div className="text-xs text-white mb-1">KONTENJANLAR</div>
                  <div className="text-2xl font-bold text-white">SINIRLI</div>
                </div>
                <div className="w-32 h-32 border-8 border-yellow-500/30 rounded-full"></div>
              </div>
            </div>
          </div>

          <div className="grid md:grid-cols-3 gap-6 mb-8">
            <SeminarCard
              badge="Sınırlı Kontenjan!"
              date="25-27 Temmuz 2024"
              title="İhale Uygulamaları ve Güncel Mevzuat Eğitimi"
              location="Kundu Deluxe Otel"
            />
            <SeminarCard
              badge="Sınırlı Kontenjan!"
              date="5 - 7 Eylül 2024"
              title="Belediyeler için Mevzuat ve Denetim Eğitimi"
              location="Luxury Resort & Spa"
            />
            <SeminarCard
              badge="Sınırlı Kontenjan!"
              date="3 - 5 Ekim 2024"
              title="Kamu İhale Mevzuatı ve Uygulamaları Eğitimi"
              location="Paradise Kundu Otel"
            />
          </div>

          <div className="text-center">
            <button className="bg-blue-600 hover:bg-blue-700 text-white px-8 py-3 rounded inline-flex items-center gap-2 transition-colors">
              Tüm Seminerleri Gör <ChevronRight className="w-5 h-5" />
            </button>
          </div>
        </div>
      </section>

      {/* Popular Trainings */}
      <section className="container mx-auto px-4 py-16">
        <h2 className="text-3xl font-bold text-gray-800 mb-6">Popüler Eğitimler</h2>
        
        {/* Tabs */}
        <div className="flex flex-wrap gap-3 mb-8 border-b border-gray-200 pb-4">
          {tabs.map((tab) => (
            <button
              key={tab}
              onClick={() => setActiveTab(tab)}
              className={`px-6 py-2 rounded-t transition-colors ${
                activeTab === tab
                  ? 'bg-blue-600 text-white'
                  : 'bg-white text-gray-700 hover:bg-gray-100'
              }`}
            >
              {tab}
            </button>
          ))}
        </div>

        {/* Training Cards Grid */}
        <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
          {trainings.map((training, index) => (
            <TrainingCard key={index} {...training} />
          ))}
        </div>

        <div className="text-center">
          <button className="bg-blue-600 hover:bg-blue-700 text-white px-8 py-3 rounded inline-flex items-center gap-2 transition-colors">
            Tüm Eğitimleri Gör <ChevronRight className="w-5 h-5" />
          </button>
        </div>
      </section>

      {/* Corporate Solutions */}
      <section className="bg-gradient-to-b from-[#1a2e4a] to-[#0f1b2e] py-16">
        <div className="container mx-auto px-4">
          <h2 className="text-white text-3xl font-bold mb-8">Kurumsal Çözümlerimiz</h2>
          
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-6">
            <div className="bg-white rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow">
              <div className="flex items-center gap-3 mb-4">
                <div className="bg-red-100 rounded-full p-3">
                  <div className="w-12 h-12 flex items-center justify-center text-red-600 text-xl font-bold">
                    TC
                  </div>
                </div>
                <div>
                  <div className="text-xs text-gray-500">T.C.</div>
                  <div className="font-semibold text-gray-800">Gümrük Hukuku&figs</div>
                </div>
              </div>
              <div className="text-sm text-gray-600">
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-1 h-1 bg-blue-600 rounded-full"></div>
                  ABC Gümrük Ushtiklik ve Müşevi
                </div>
              </div>
            </div>

            <div className="bg-white rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow">
              <div className="flex items-center gap-3 mb-4">
                <div className="bg-blue-100 rounded-full p-3">
                  <div className="w-12 h-12 flex items-center justify-center">
                    <div className="text-blue-600 text-2xl">🏛️</div>
                  </div>
                </div>
                <div>
                  <div className="font-semibold text-gray-800">İstanbul<br />Denetmesing</div>
                </div>
              </div>
              <div className="text-sm text-gray-600">
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-1 h-1 bg-blue-600 rounded-full"></div>
                  XYZ İçurum ve Mevzuela
                </div>
              </div>
            </div>

            <div className="bg-white rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow">
              <div className="flex items-center gap-3 mb-4">
                <div className="bg-orange-100 rounded-full p-3">
                  <div className="w-12 h-12 flex items-center justify-center text-orange-600 text-2xl font-bold">
                    🏢
                  </div>
                </div>
                <div className="font-semibold text-gray-800">Arcelik</div>
              </div>
              <div className="text-sm text-gray-600">
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-1 h-1 bg-blue-600 rounded-full"></div>
                  Tesda Güryemimizing Başın
                </div>
              </div>
            </div>

            <div className="bg-white rounded-lg p-6 shadow-lg hover:shadow-xl transition-shadow">
              <div className="flex items-center gap-3 mb-4">
                <div className="bg-red-50 rounded-full p-3">
                  <svg className="w-12 h-12" viewBox="0 0 100 40">
                    <text x="10" y="25" fill="#dc2626" className="text-lg font-bold">arcelik</text>
                  </svg>
                </div>
              </div>
              <div className="text-sm text-gray-600">
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-1 h-1 bg-blue-600 rounded-full"></div>
                  Neslie Damyashing Başger Mücait
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-[#1a2e4a] text-white py-16">
        <div className="container mx-auto px-4">
          <div className="grid md:grid-cols-2 lg:grid-cols-4 gap-8 mb-12">
            {/* Company Info */}
            <div>
              <div className="flex items-center gap-3 mb-6">
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
              <p className="text-gray-300 text-sm mb-4">
                Profesyonel eğitim ve seminerler ile kamu ve özel sektöre hizmet veriyoruz.
              </p>
              <div className="flex gap-3">
                <a href="#" className="bg-white/10 hover:bg-orange-500 p-2 rounded transition-colors">
                  <Facebook className="w-5 h-5" />
                </a>
                <a href="#" className="bg-white/10 hover:bg-orange-500 p-2 rounded transition-colors">
                  <Twitter className="w-5 h-5" />
                </a>
                <a href="#" className="bg-white/10 hover:bg-orange-500 p-2 rounded transition-colors">
                  <Linkedin className="w-5 h-5" />
                </a>
                <a href="#" className="bg-white/10 hover:bg-orange-500 p-2 rounded transition-colors">
                  <Instagram className="w-5 h-5" />
                </a>
              </div>
            </div>

            {/* Kurumsal */}
            <div>
              <h3 className="text-lg font-semibold mb-4">Kurumsal</h3>
              <ul className="space-y-3">
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Hakkımızda
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Misyon & Vizyon
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Eğitmenlerimiz
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Tesisimiz
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Referanslarımız
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Kariyer
                  </a>
                </li>
              </ul>
            </div>

            {/* Eğitimler */}
            <div>
              <h3 className="text-lg font-semibold mb-4">Eğitimler</h3>
              <ul className="space-y-3">
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Mevzuat & Hukuk
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Teknoloji & Dijital Dönüşüm
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Kişisel Gelişim
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Online Eğitimler
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Antalya Seminerleri
                  </a>
                </li>
                <li>
                  <a href="#" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    Eğitim Takvimi
                  </a>
                </li>
              </ul>
            </div>

            {/* İletişim */}
            <div>
              <h3 className="text-lg font-semibold mb-4">İletişim</h3>
              <ul className="space-y-4">
                <li className="flex items-start gap-3">
                  <MapPin className="w-5 h-5 text-orange-400 flex-shrink-0 mt-0.5" />
                  <span className="text-gray-300 text-sm">
                    Atatürk Bulvarı No:123<br />
                    Çankaya, Ankara
                  </span>
                </li>
                <li className="flex items-center gap-3">
                  <Phone className="w-5 h-5 text-orange-400 flex-shrink-0" />
                  <a href="tel:+902121234567" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    +90 (212) 123 45 67
                  </a>
                </li>
                <li className="flex items-center gap-3">
                  <Mail className="w-5 h-5 text-orange-400 flex-shrink-0" />
                  <a href="mailto:info@gortepe.com" className="text-gray-300 hover:text-orange-400 text-sm transition-colors">
                    info@gortepe.com
                  </a>
                </li>
              </ul>
              <div className="mt-6">
                <button className="bg-orange-500 hover:bg-orange-600 text-white px-6 py-2 rounded text-sm transition-colors w-full">
                  İletişime Geç
                </button>
              </div>
            </div>
          </div>

          {/* Bottom Bar */}
          <div className="border-t border-white/10 pt-8">
            <div className="flex flex-col md:flex-row justify-between items-center gap-4">
              <p className="text-sm text-gray-400">
                © 2024 GorTepe Müşavirlik Hizmetleri. Tüm hakları saklıdır.
              </p>
              <div className="flex gap-6 text-sm">
                <a href="#" className="text-gray-400 hover:text-orange-400 transition-colors">
                  Gizlilik Politikası
                </a>
                <a href="#" className="text-gray-400 hover:text-orange-400 transition-colors">
                  Kullanım Koşulları
                </a>
                <a href="#" className="text-gray-400 hover:text-orange-400 transition-colors">
                  KVKK
                </a>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
}
